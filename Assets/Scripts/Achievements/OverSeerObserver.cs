using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OverSeerObserver: MonoBehaviour
{

    private SerializableDictionary<OverSeerEvent, List<System.Action>> _eventListeners;
    private OverSeerObserver()
    {
        _eventListeners = new SerializableDictionary<OverSeerEvent, List<System.Action>>();
        foreach (OverSeerEvent type in System.Enum.GetValues(typeof(OverSeerEvent)))
        {
            _eventListeners[type] = new List<System.Action>();
        }
    }
    private static OverSeerObserver _instance;

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

    public void AddListener(OverSeerEvent type, System.Action listener)
    {
        _eventListeners[type].Add(listener);
    }

    public static void Notify(OverSeerEvent type){
        Instance.ProcessEvent(type);
    }

    private IEnumerable ProcessEvent(OverSeerEvent type){
        switch(type){
            case OverSeerEvent.SoldBeer:
                
                break;
        }
        return null;
    }



}

public enum OverSeerEvent
{
    SoldBeer,
}