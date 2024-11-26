using UnityEngine;
using TMPro;

public class BarInteractable : Interactable
{
    public int initialStock = 5;
    public TextMeshProUGUI beerDisplay;

    public int Stock
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

    private int stock;

    public override bool Interact()
    {
        return InteractFunctionality.Interact();
    }

    public override bool NPCInteract(GameObject npc)
    {
        return InteractFunctionality.NPCInteract(npc);
    }

    public override Sprite GetHoverIcon()
    {
        return InteractFunctionality.GetHoverIcon();
    }

    public void SetStartingStock()
    {
        Stock = initialStock;
    }
}
