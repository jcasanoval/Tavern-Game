using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TableInteractuable : Interactable
{
    private List<GameObject> furniture;

    private GameObject positionMarker;

    [SerializeField]
    private bool isActive = false;

    [SerializeField]
    [Range(1, 20)]
    private int price = 5;

    // Start is called before the first frame update
    void Start()
    {
        FindFurniture();
        SetFurniture(isActive);
    }

    private void FindFurniture()
    {
        furniture = new List<GameObject>();

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.tag == "Furniture")
            {
                furniture.Add(transform.GetChild(i).gameObject);
            }
            else
            {
                positionMarker = transform.GetChild(i).gameObject;
            }

        }
    }

    private void SetFurniture(bool active)
    {
        foreach (GameObject item in furniture)
        {
            item.SetActive(active);
        }

        positionMarker.SetActive(!active);

        FindObjectOfType<ChairManager>().RefreshChairs();

        isActive = active;
    }

    public override void Interact()
    {
        if (FindObjectOfType<DayCycleManager>().IsOpen())
        {
            Debug.Log("Cannot purchase table while open");
            return;
        }

        if (isActive)
        {
            Debug.Log("Table already purchased");
            return;
        }

        Debug.Log("Interacting with table");
        var madePurchase = FindAnyObjectByType<GoldManager>().SpendGold(price);

        if (madePurchase)
        {
            Debug.Log("Table purchased");
            SetFurniture(true);
        }
        else
        {
            Debug.Log("Not enough gold to purchase table");
        }
    }
}
