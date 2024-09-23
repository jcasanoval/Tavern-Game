using UnityEngine;
using TMPro;

public class Barrel : Interactable
{
    private HandController handController;
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
    }

    public override void Interact()
    {
        if (handController.HasFreeHands() && Stock > 0)
        {
            Stock--;
            handController.HoldMug();
        }
    }
}
