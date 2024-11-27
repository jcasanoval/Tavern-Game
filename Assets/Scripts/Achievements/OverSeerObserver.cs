using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OverSeerObserver: MonoBehaviour
{

    private Dictionary<OverSeerEvent, List<Tuple<System.Action,Guid>>> _eventListeners;
    private OverSeerObserver()
    {
        _eventListeners = new Dictionary<OverSeerEvent, List<Tuple<System.Action,Guid>>>();
        foreach (OverSeerEvent type in System.Enum.GetValues(typeof(OverSeerEvent)))
        {
            _eventListeners[type] = new List<Tuple<System.Action,Guid>>();
        }
    }
    private static OverSeerObserver _instance;

    private void Awake()
    {
        _instance = this;
        _eventListeners = new Dictionary<OverSeerEvent, List<Tuple<System.Action,Guid>>>();
        foreach (OverSeerEvent type in System.Enum.GetValues(typeof(OverSeerEvent)))
        {
            _eventListeners[type] = new List<Tuple<System.Action,Guid>>();
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

    public Guid AddListener(OverSeerEvent type, System.Action listener)
    {
        Guid id = Guid.NewGuid();
        _eventListeners[type].Add(new Tuple<System.Action, Guid>(listener, id));
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
        print("Sold Beer Event");
        foreach(var listener in _eventListeners[type]){
            print("Invoking Listener:" + listener.Item2 + " " + type);
            listener.Item1();
        }
        yield return null;
    }



}

public enum OverSeerEvent
{
    SoldBeer,
}