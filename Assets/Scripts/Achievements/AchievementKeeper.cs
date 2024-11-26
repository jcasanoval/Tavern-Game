using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementKeeper : MonoBehaviour
{
    private LedgerOfStats _ledger;

    private void Start()
    {
        _ledger = LedgerOfStats.Instance;
    }

    private void AccountForBeersSold()
    {
        Guid id = Guid.Empty;
        Action action =  delegate() 
        {
            _ledger.BeersSold++;
            if(_ledger.BeersSold == 10)
            {
                Debug.Log("Achievement Unlocked: 10 Beers Sold");
                OverSeerObserver.Instance.RemoveListener(OverSeerEvent.SoldBeer, id);
            }
        };
        id = OverSeerObserver.Instance.AddListener(OverSeerEvent.SoldBeer,action);
    }
}

public class LedgerOfStats
{
    private static LedgerOfStats _instance;
    public static LedgerOfStats Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new LedgerOfStats();
            }
            return _instance;
        }
    }

    private int _beersSold;
    public int BeersSold
    {
        get
        {
            return _beersSold;
        }
        set
        {
            _beersSold = value;
        }
    }

    private LedgerOfStats()
    {
        _beersSold = 0;
    }


}