using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeManagerInteraction : IInteractFunctionality
{    
    private DayCycleManager dayCycleManager;
    private GoldManager goldManager;
    private EmployeeManager employeeManager;
    public Sprite hoverIcon;
    private PlayerInteraction playerInteraction;

    public void Start() {
        dayCycleManager = FindObjectOfType<DayCycleManager>();
        goldManager = FindObjectOfType<GoldManager>();
        employeeManager = FindObjectOfType<EmployeeManager>();
        playerInteraction = FindObjectOfType<PlayerInteraction>();
    }
    public override bool Interact()
    {
        if (dayCycleManager.IsOpen())
        {
            return false;
        }
        else if (goldManager.SpendGold(employeeManager.costToBuyDude))
        {
            employeeManager.SummonEmployee();
            if (!goldManager.CanSpendGold(employeeManager.costToBuyDude)) {
                playerInteraction.HideHover(employeeManager);
            }
            return true;
        }
        return false;
    }

    public override Sprite GetHoverIcon()
    {
        if (!dayCycleManager.IsOpen() && goldManager.CanSpendGold(employeeManager.costToBuyDude)) {
            return hoverIcon;
        }

        return null;
    }
}