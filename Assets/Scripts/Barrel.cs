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
        print("NPC " + npc.name + "Interacting with " + gameObject.name);
        if(npc.tag != "Employee"){
            return false;
        }
        Employee employee = npc.GetComponent<Employee>();
        if (Stock > 0 && !employee.HasBeer)
        {
            Stock--;
            employee.HasBeer = true;
            return true;
        }
        return false;
    }
}
