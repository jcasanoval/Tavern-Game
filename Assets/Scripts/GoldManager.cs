using UnityEngine;

public class GoldManager : MonoBehaviour
{
    private int Gold
    {
        get
        {
            return gold;
        }
        set
        {
            gold = value;
            // Update UI
        }
    }

    private int gold = 0;

    public void AddGold(int amount)
    {
        gold += amount;
    }

    public void SpendGold(int amount)
    {
        gold -= amount;
    }
}
