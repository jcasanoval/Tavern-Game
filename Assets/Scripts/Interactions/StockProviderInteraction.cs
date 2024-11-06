using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockProviderInteraction : IInteractFunctionality
{
    private DayCycleManager dayCycleManager;
    public BarInteractable barInteractable;
    private GoldManager goldManager;

    void Awake()
    {
        dayCycleManager = FindObjectOfType<DayCycleManager>();
        goldManager = FindObjectOfType<GoldManager>();
    }

    public override bool Interact()
    {
        if (!dayCycleManager.IsOpen() && goldManager.SpendGold(1))
        {
            barInteractable.Stock++;
            return true;
        }
        return false;
    }
}
