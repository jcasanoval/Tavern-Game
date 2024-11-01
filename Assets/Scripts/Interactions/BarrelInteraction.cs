using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelInteraction : IInteractFunctionality
{
    private HandController handController;
    private DayCycleManager dayCycleManager;
    private Barrel barrel;
    private GoldManager goldManager;
    private AudioSource serveBeerAudioSource;

    void Awake()
    {
        barrel = GetComponentInParent<Barrel>();
        serveBeerAudioSource = barrel.GetComponent<AudioSource>();
        handController = FindObjectOfType<HandController>();
        dayCycleManager = FindObjectOfType<DayCycleManager>();
        goldManager = FindObjectOfType<GoldManager>();
    }

    public override bool Interact()
    {
        if (dayCycleManager.IsOpen())
        {
            if (handController.HasFreeHands() && barrel.Stock > 0)
            {
                barrel.Stock--;
                handController.HoldMug();
                serveBeerAudioSource.Play();
                return true;
            }
        }
        else if (goldManager.SpendGold(1))
        {
            barrel.Stock++;
            return true;
        }
        return false;
    }
}
