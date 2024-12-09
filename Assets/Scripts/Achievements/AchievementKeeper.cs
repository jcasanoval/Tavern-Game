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
        ListenerLoading();
    }

    private void ListenerLoading(){
        BeerCounter();
        AccountForBeersSold();
        ThirtyBeersSold();
        TutorialCompleted();
        NightEnd();
        CustomerHandling();
        MoneyHandling();
        TableHandling();
        BartenderHandling();
        OutsideAchievements();
        FallingAchievements();
    }


    public void AchievementReset()
    {   
        LedgerOfStats.Instance.ResetLedger();
        _ledger = LedgerOfStats.Instance;
        OverSeerObserver.Instance.ResetOverseer();
        ListenerLoading();
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
            _ledger.Popularity = GetComponent<PopularityManager>().Popularity;
            _ledger.RegisterNightStats();
            float night = _ledger.Night;
            LedgerRecord record = _ledger.GetRecordByNight((int)night);
            LedgerRecord previousRecord = _ledger.GetRecordByNight((int)night - 1);

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
                    Popularity = 0,
                    Tables = 0
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

        Func<bool> action2 = delegate()
        {
            if(_ledger.Night > 14)
            {
                Debug.Log("Achievement Unlocked: Night Ended");
                AchievementPopUp.Instance.RewardAchievement(new Achievement()
                {
                    Title = "Una y otra vez",
                    Icon = null
                });
            }
            return false;
        };
    }

    private void CustomerHandling(){
        Func<bool> action = delegate()
        {
            LedgerOfStats.Instance.CustomerVisits++;
            return true;
        };

        OverSeerObserver.Instance.AddListener(OverSeerEvent.Customer_Arrived,action);

        Func<bool> action2 = delegate()
        {
            _ledger.CustomerLeaves++;
            return true;
        };

        OverSeerObserver.Instance.AddListener(OverSeerEvent.Customer_Leaves,action2);

        Func<bool> action3 = delegate()
        {
            if(_ledger.CustomerLeaves > 7)
            {
                Debug.Log("Achievement Unlocked: Customer Left Angry");
                AchievementPopUp.Instance.RewardAchievement(new Achievement()
                {
                    Title = "NO LLEGUE!!",
                    Icon = null
                });
            }
            return false;
        };

        OverSeerObserver.Instance.AddListener(OverSeerEvent.Customer_Leaves,action3);
    }

    private void MoneyHandling(){
        Func<bool> action = delegate()
        {
            _ledger.MoneyEarned++;
            return true;
        };

        OverSeerObserver.Instance.AddListener(OverSeerEvent.Money_Earned,action);

        Func<bool> action2 = delegate()
        {
            _ledger.MoneySpent++;
            return true;
        };

        OverSeerObserver.Instance.AddListener(OverSeerEvent.Money_Spent,action2);
    }

    private void TableHandling(){
        Func<bool> action = delegate()
        {
            _ledger.Tables++;
            return true;
        };

        OverSeerObserver.Instance.AddListener(OverSeerEvent.Table_Bought,action);

        Func<bool> action2 = delegate()
        {
            if(_ledger.Tables > 0)
            {
                Debug.Log("Achievement Unlocked: More, more, much more!");
                AchievementPopUp.Instance.RewardAchievement(new Achievement()
                {
                    Title = "Más, más, mucho mas!",
                    Icon = null
                });
            }
            return false;
        };

        OverSeerObserver.Instance.AddListener(OverSeerEvent.Table_Bought,action2);

        Func<bool> action3 = delegate()
        {
            if(_ledger.Tables > 2)
            {
                Debug.Log("Achievement Unlocked: Fiesta completa!");
                AchievementPopUp.Instance.RewardAchievement(new Achievement()
                {
                    Title = "Fiesta completa!",
                    Icon = null
                });
            }
            return false;
        };

        OverSeerObserver.Instance.AddListener(OverSeerEvent.Table_Bought,action3);
    }

    private void BartenderHandling(){
        Func<bool> action = delegate()
        {
            _ledger.Bartenders++;
            return true;
        };

        OverSeerObserver.Instance.AddListener(OverSeerEvent.Hire_Bartender,action);

        Func<bool> action2 = delegate()
        {
            if(_ledger.Bartenders > 0)
            {
                Debug.Log("Achievement Unlocked: Bartender hired");
                AchievementPopUp.Instance.RewardAchievement(new Achievement()
                {
                    Title = "Ayuda, Ayuda!",
                    Icon = null
                });
            }
            return false;
        };

        OverSeerObserver.Instance.AddListener(OverSeerEvent.Hire_Bartender,action2);

        Func<bool> action3 = delegate()
        {
            if(_ledger.Bartenders > 3)
            {
                Debug.Log("Achievement Unlocked: Bartender hired");
                AchievementPopUp.Instance.RewardAchievement(new Achievement()
                {
                    Title = "Tu propia tropa",
                    Icon = null
                });
            }
            return false;
        };

        OverSeerObserver.Instance.AddListener(OverSeerEvent.Hire_Bartender,action3);
    }

    private void OutsideAchievements(){
        Func<bool> action = delegate()
        {
            Debug.Log("Achievement Unlocked: Outside Achievement");
            AchievementPopUp.Instance.RewardAchievement(new Achievement()
            {
                Title = "Y como entro?...",
                Icon = null
            });
            return false;
        };

        OverSeerObserver.Instance.AddListener(OverSeerEvent.TrappedOutside,action);

        Func<bool> action2 = delegate()
        {
            Debug.Log("Achievement Unlocked: Outside Achievement");
            AchievementPopUp.Instance.RewardAchievement(new Achievement()
            {
                Title = "Oh! bendita Excalibur",
                Icon = null
            });
            return false;
        };

        OverSeerObserver.Instance.AddListener(OverSeerEvent.Excalibur_Used,action2);

        Func<bool> action3 = delegate()
        {
            Debug.Log("Achievement Unlocked: Outside Achievement");
            AchievementPopUp.Instance.RewardAchievement(new Achievement()
            {
                Title = "El elegido del Lago",
                Icon = null
            });
            return false;
        };

        OverSeerObserver.Instance.AddListener(OverSeerEvent.Excalibur_Unsheathe,action3);

        Func<bool> action4 = delegate()
        {
            Debug.Log("Achievement Unlocked: Outside Achievement");
            AchievementPopUp.Instance.RewardAchievement(new Achievement()
            {
                Title = "Meh...",
                Icon = null
            });
            return false;
        };

        OverSeerObserver.Instance.AddListener(OverSeerEvent.Excalibur_Sheathe,action4);
    }

    private void FallingAchievements(){
        Func<bool> action = delegate()
        {
            Debug.Log("Achievement Unlocked: Falling Achievement");
            AchievementPopUp.Instance.RewardAchievement(new Achievement()
            {
                Title = "Perdí una ficha...",
                Icon = null
            });
            return false;
        };

        OverSeerObserver.Instance.AddListener(OverSeerEvent.Fall_Off_Table,action);
    }

}
