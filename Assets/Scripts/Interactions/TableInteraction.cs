using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableInteraction : IInteractFunctionality
{
    private List<GameObject> furniture;

    private GameObject positionMarker;
    private TableInteractuable table;
    private DayCycleManager dayCycleManager;
    public Sprite hoverIcon;
    private PlayerInteraction playerInteraction;

    public bool startsActive;

    private bool isActive;

    [SerializeField]
    [Range(1, 20)]
    private int price = 5;

    // Start is called before the first frame update
    void Start()
    {
        isActive = startsActive;
        table = GetComponentInParent<TableInteractuable>();
        FindFurniture();
        SetFurniture(isActive);
        dayCycleManager = FindObjectOfType<DayCycleManager>();
        playerInteraction = FindObjectOfType<PlayerInteraction>();
    }

    private void FindFurniture()
    {
        furniture = new List<GameObject>();

        for (int i = 0; i < table.transform.childCount; i++)
        {
            if (table.transform.GetChild(i).gameObject.tag == "Furniture")
            {
                furniture.Add(table.transform.GetChild(i).gameObject);
            }
            else
            {
                positionMarker = table.transform.GetChild(i).gameObject;
            }

        }
    }

    public void SetFurniture(bool active)
    {
        foreach (GameObject item in furniture)
        {
            item.SetActive(active);
        }

        positionMarker.SetActive(!active);

        FindObjectOfType<ChairManager>().RefreshChairs();

        isActive = active;
    }

    public override bool Interact()
    {
        if (dayCycleManager.IsOpen())
        {
            Debug.Log("Cannot purchase table while open");
            return false;
        }

        if (isActive)
        {
            Debug.Log("Table already purchased");
            return false;
        }

        Debug.Log("Interacting with table");
        var madePurchase = FindAnyObjectByType<GoldManager>().SpendGold(price);

        if (!madePurchase)
        {
            Debug.Log("Not enough gold to purchase table");
            return false;
        }

        Debug.Log("Table purchased");
        OverSeerObserver.Instance.Notify(OverSeerEvent.Table_Bought);
        playerInteraction.HideHover(table);
        SetFurniture(true);
        table.GetComponent<TableAnimation>().Animate();
        return true;
    }

    public override Sprite GetHoverIcon()
    {
        if (!dayCycleManager.IsOpen() && !isActive && FindAnyObjectByType<GoldManager>().CanSpendGold(price))
        {
            return hoverIcon;
        }
        return null;
    }
}
