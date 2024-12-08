using System;
using System.Collections.Generic;
using UnityEngine;

public class AchievementKeeper : MonoBehaviour
{
    private LedgerOfStats _ledger;
    [SerializeField]
    private Sprite tutorialCompletedIcon; //TODO: delete from here and use a dictionary or something else
    private TextAccess EODTextAccess;

    private void Start()
    {
        _ledger = LedgerOfStats.Instance;
        EODTextAccess = FindObjectOfType<TextAccess>();
        BeerCounter();
        AccountForBeersSold();
        ThirtyBeersSold();
        TutorialCompleted();
        NightEnd();
        CustomerHandling();
        MoneyHandling();
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
                    Title = "10 Cervezas Vendidas",
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
                    Title = "30 Cervezas Vendidas",
                    Icon = null
                });
                return false;
            }
            return true;
        };
        id = OverSeerObserver.Instance.AddListener(OverSeerEvent.SoldBeer,action);
        Debug.Log("Listener Added:" + id);
    }

    private void TutorialCompleted()
    {
        Guid id = Guid.Empty;
        Func<bool> action = delegate()
        {
            Debug.Log("Achievement Unlocked: Tutorial completed");
            AchievementPopUp.Instance.RewardAchievement(new Achievement()
            {
                Title = "Tutorial Completado",
                Icon = tutorialCompletedIcon
            });
            return false;
        };
        id = OverSeerObserver.Instance.AddListener(OverSeerEvent.TutorialCompleted,action);
        Debug.Log("Listener Added:" + id);
    }

    private void NightEnd(){
        Func<bool> action = delegate()
        {
            Debug.Log("Achievement Unlocked: Night Ended");
            LedgerOfStats.Instance.Popularity = GetComponent<PopularityManager>().Popularity;
            LedgerOfStats.Instance.RegisterNightStats();
            float night = LedgerOfStats.Instance.Night;
            LedgerRecord record = LedgerOfStats.Instance.GetRecordByNight((int)night);
            LedgerRecord previousRecord = LedgerOfStats.Instance.GetRecordByNight((int)night - 1);

            if (previousRecord == null)
            {
                previousRecord = new LedgerRecord()
                {
                    BeersSold = 0,
                    Night = 0,
                    Money_Earned = 0,
                    Money_Spent = 0,
                    Customer_Left_Angry = 0,
                    Total_Customers = 0,
                    Popularity = 0
                };
            }

            EODTextAccess.MoneyGained = record.Money_Earned - previousRecord.Money_Earned;
            EODTextAccess.MoneySpent = record.Money_Spent - previousRecord.Money_Spent;
            EODTextAccess.Total = EODTextAccess.MoneyGained - EODTextAccess.MoneySpent;
            EODTextAccess.ClientsServed = record.BeersSold - previousRecord.BeersSold;
            EODTextAccess.ClientsLost = record.Customer_Left_Angry - previousRecord.Customer_Left_Angry;
            EODTextAccess.Popularity = record.Popularity;

            FindAnyObjectByType<MenuCamera>().CameraState = CameraState.EndOfDay;
            
            return true;
        };

        OverSeerObserver.Instance.AddListener(OverSeerEvent.NightEnd,action);
    }

    private void CustomerHandling(){
        Func<bool> action = delegate()
        {
            Debug.Log("Achievement Unlocked: Customer Arrived");
            LedgerOfStats.Instance.CustomerVisits++;
            return true;
        };

        OverSeerObserver.Instance.AddListener(OverSeerEvent.Customer_Arrived,action);

        Func<bool> action2 = delegate()
        {
            Debug.Log("Achievement Unlocked: Customer Left Angry");
            LedgerOfStats.Instance.CustomerLeaves++;
            return true;
        };

        OverSeerObserver.Instance.AddListener(OverSeerEvent.Customer_Leaves,action2);
    }

    private void MoneyHandling(){
        Func<bool> action = delegate()
        {
            Debug.Log("Achievement Unlocked: Money Earned");
            LedgerOfStats.Instance.MoneyEarned++;
            return true;
        };

        OverSeerObserver.Instance.AddListener(OverSeerEvent.Money_Earned,action);

        Func<bool> action2 = delegate()
        {
            Debug.Log("Achievement Unlocked: Money Spent");
            LedgerOfStats.Instance.MoneySpent++;
            return true;
        };

        OverSeerObserver.Instance.AddListener(OverSeerEvent.Money_Spent,action2);
    }

}
