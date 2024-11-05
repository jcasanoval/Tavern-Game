using UnityEngine;
using TMPro;

public class Barrel : Interactable
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

    void Start()
    {
        Stock = initialStock;
    }

    public override bool Interact()
    {
        return InteractFunctionality.Interact();
    }

    public override bool NPCInteract(GameObject npc)
    {
        return InteractFunctionality.NPCInteract(npc);
    }
}
