using TMPro;
using UnityEngine;

public class GoldManager : MonoBehaviour
{
    public TextMeshProUGUI goldDisplay;
    public int startingGold = 5;
    private int Gold
    {
        get
        {
            return gold;
        }
        set
        {
            int oldValue = gold;
            gold = value;
            goldDisplay.text = "$" + gold.ToString();
            if (oldValue - value > 0)
            {
                for (int i = oldValue; i > value; i--)
                {
                    OverSeerObserver.Instance.Notify(OverSeerEvent.Money_Spent);
                }
            }
            else
            {
                for (int i = oldValue; i < value; i++)
                {
                    OverSeerObserver.Instance.Notify(OverSeerEvent.Money_Earned);
                }
            }
        }
    }

    private int gold = 0;

    private void Start()
    {
        SetInitialGold();
        goldDisplay.text = "$" + gold.ToString();
    }

    public void SetInitialGold()
    {
        gold = startingGold;
    }

    public void AddGold(int amount)
    {
        Gold += amount;
    }

    public bool SpendGold(int amount)
    {
        if (Gold < amount)
        {
            return false;
        }
        Gold -= amount;
        return true;
    }

    public bool CanSpendGold(int amount)
    {
        return Gold >= amount;
    }
}
