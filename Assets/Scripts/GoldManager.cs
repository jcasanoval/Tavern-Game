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
            gold = value;
            goldDisplay.text = "$" + gold.ToString();
        }
    }

    private int gold = 0;

    private void Start()
    {
        SetInitialGold();
    }

    public void SetInitialGold()
    {
        Gold += startingGold;
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
