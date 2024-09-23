using UnityEngine;
using TMPro;

public class Barrel : Interactable
{
    private HandController handController;
    private DayCycleManager dayCycleManager;
    private GoldManager goldManager;
    public TextMeshProUGUI beerDisplay;

    private int Stock
    {
        get
        {
            return stock;
        }
        set
        {
            stock = value;
            beerDisplay.text = stock.ToString();
        }
    }

    private int stock = 0;

    void Start()
    {
        Stock = 10;
        handController = FindObjectOfType<HandController>();
        dayCycleManager = FindObjectOfType<DayCycleManager>();
        goldManager = FindObjectOfType<GoldManager>();
    }

    public override void Interact()
    {
        if (dayCycleManager.IsOpen())
        {
            if (handController.HasFreeHands() && Stock > 0)
            {
                Stock--;
                handController.HoldMug();
            }
        }
        else if (goldManager.SpendGold(1))
        {
            Stock++;
        }
    }
}
