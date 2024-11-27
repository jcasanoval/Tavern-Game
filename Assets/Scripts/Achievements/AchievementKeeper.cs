using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class AchievementKeeper : MonoBehaviour
{
    private LedgerOfStats _ledger;

    private void Start()
    {
        _ledger = LedgerOfStats.Instance;
        BeerCounter();
        AccountForBeersSold();
        ThirtyBeersSold();
    }

    private void BeerCounter(){
        Func<bool> action = delegate()
        {
            _ledger.BeersSold++;
            return true;
        };
        OverSeerObserver.Instance.AddListener(OverSeerEvent.SoldBeer,action);
    }

    private void AccountForBeersSold()
    {
        Guid id = Guid.Empty;
        Func<bool> action = delegate()
        {
            if(_ledger.BeersSold == 10)
            {
                Debug.Log("Achievement Unlocked: 10 Beers Sold");
                //OverSeerObserver.Instance.RemoveListener(OverSeerEvent.SoldBeer, id);
                AchievementPopUp.Instance.RewardAchievement(new Achievement()
                {
                    Title = "10 Beers Sold",
                    Icon = null
                });
                return false;
            }
            return true;
        };
        id = OverSeerObserver.Instance.AddListener(OverSeerEvent.SoldBeer,action);
        Debug.Log("Listener Added:" + id);
    }

    private void ThirtyBeersSold()
    {
        Guid id = Guid.Empty;
        Func<bool> action = delegate()
        {
            if(_ledger.BeersSold == 30)
            {
                Debug.Log("Achievement Unlocked: 30 Beers Sold");
                //OverSeerObserver.Instance.RemoveListener(OverSeerEvent.SoldBeer, id);
                AchievementPopUp.Instance.RewardAchievement(new Achievement()
                {
                    Title = "30 Beers Sold",
                    Icon = null
                });
                return false;
            }
            return true;
        };
        id = OverSeerObserver.Instance.AddListener(OverSeerEvent.SoldBeer,action);
        Debug.Log("Listener Added:" + id);
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
            Debug.Log("Beers Sold: " + _beersSold);
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