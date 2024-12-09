using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgerOfStats
{
    private static LedgerOfStats _instance;

    private Dictionary<Record,float> _stats;

    public static List<LedgerRecord> Records { get; set; }
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

    public LedgerOfStats()
    {
        _stats = new Dictionary<Record, float>();
        Records = new List<LedgerRecord>();
        foreach (Record record in System.Enum.GetValues(typeof(Record)))
        {
            _stats.Add(record, 0);
        }
    }

    public void ResetLedger()
    {
        foreach (Record record in System.Enum.GetValues(typeof(Record)))
        {
            _stats[record] = 0;
        }
        Records.Clear();
    }

    public float BeersSold
    {
        get
        {
            return _stats[Record.BeersSold];
        }
        set
        {
            _stats[Record.BeersSold] = value;
        }
    }
    public float Night
    {
        get
        {
            return _stats[Record.Night];
        }
        set
        {
            _stats[Record.Night] = value;
        }
    }


    public float CustomerVisits
    {
        get
        {
            return _stats[Record.Total_Customers];
        }
        set
        {
            _stats[Record.Total_Customers] = value;
        }
    }


    public float CustomerLeaves
    {
        get
        {
            return _stats[Record.Customer_Left_Angry];
        }
        set
        {
            _stats[Record.Customer_Left_Angry] = value;
        }
    }


    public float MoneyEarned
    {
        get
        {
            return _stats[Record.Money_Earned];
        }
        set
        {
            _stats[Record.Money_Earned] = value;
        }
    }


    public float MoneySpent
    {
        get
        {
            return _stats[Record.Money_Spent];
        }
        set
        {
            _stats[Record.Money_Spent] = value;
        }
    }

    public float Popularity
    {
        get
        {
            return _stats[Record.Popularity];
        }
        set
        {
            _stats[Record.Popularity] = value;
        }
    }

    public float Tables
    {
        get
        {
            return _stats[Record.Tables];
        }
        set
        {
            _stats[Record.Tables] = value;
        }
    }

    public float Bartenders
    {
        get
        {
            return _stats[Record.Bartenders];
        }
        set
        {
            _stats[Record.Bartenders] = value;
        }
    }

    public void RegisterNightStats()
    {
        Night++;
        LedgerRecord record = new LedgerRecord()
        {
            BeersSold = _stats[Record.BeersSold],
            Night = _stats[Record.Night],
            Money_Earned = _stats[Record.Money_Earned],
            Money_Spent = _stats[Record.Money_Spent],
            Customer_Left_Angry = _stats[Record.Customer_Left_Angry],
            Total_Customers = _stats[Record.Total_Customers],
            Popularity = _stats[Record.Popularity],
            Tables = _stats[Record.Tables]
        };

        Records.Add(record);
    }

    public LedgerRecord GetRecordByNight(int night){
        return Records.Find(x => x.Night == night);
    }


}

public enum Record
{
    BeersSold,
    Night,
    Customer_Left_Angry,
    Money_Earned,
    Money_Spent,
    Total_Customers,
    Popularity,
    Tables,
    Bartenders

}
