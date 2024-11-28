using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OverSeerObserver: MonoBehaviour
{

    private Dictionary<OverSeerEvent, List<Tuple<Func<bool>,Guid>>> _eventListeners;
    private Dictionary<OverSeerEvent, Tuple<bool,bool>> _locksRW;
    private OverSeerObserver()
    {
        _eventListeners = new Dictionary<OverSeerEvent, List<Tuple<Func<bool>,Guid>>>();
        _locksRW = new Dictionary<OverSeerEvent, Tuple<bool, bool>>();
        foreach (OverSeerEvent type in System.Enum.GetValues(typeof(OverSeerEvent)))
        {
            _eventListeners[type] = new List<Tuple<Func<bool>,Guid>>();
            _locksRW[type] = new Tuple<bool, bool>(false, false);
        }
    }
    private static OverSeerObserver _instance;

    private void Awake()
    {
        _instance = this;
        _eventListeners = new Dictionary<OverSeerEvent, List<Tuple<Func<bool>,Guid>>>();
        foreach (OverSeerEvent type in System.Enum.GetValues(typeof(OverSeerEvent)))
        {
            _eventListeners[type] = new List<Tuple<Func<bool>,Guid>>();
        }
    }

    public static OverSeerObserver Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new OverSeerObserver();
            }
            return _instance;
        }
    }

    public Guid AddListener(OverSeerEvent type, Func<bool> listener)
    {
        Guid id = Guid.NewGuid();
        _eventListeners[type].Add(new Tuple<Func<bool>, Guid>(listener, id));
        return id;
    }

    public void RemoveListener(OverSeerEvent type, Guid id)
    {
        _eventListeners[type].RemoveAll(x => x.Item2 == id);
    }

    public void Notify(OverSeerEvent type){
        StartCoroutine(Instance.ProcessEvent(type));
    }

    private IEnumerator ProcessEvent(OverSeerEvent type){
        print("Sold Beer Event "+ _eventListeners[type].Count);
        List<Guid> toRemove = new List<Guid>();
        foreach(var listener in _eventListeners[type]){
            print("Invoking Listener:" + listener.Item2 + " " + type);
            if (!listener.Item1())
            {
                toRemove.Add(listener.Item2);
            }
        }
        lock(_eventListeners[type]){
            foreach(var id in toRemove){
                _eventListeners[type].RemoveAll(x => x.Item2 == id);
            }
        }
        yield return null;
    }



}

public enum OverSeerEvent
{
    SoldBeer,
    TutorialCompleted
}