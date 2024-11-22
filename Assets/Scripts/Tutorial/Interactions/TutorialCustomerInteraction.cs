using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCustomerInteraction : IInteractFunctionality
{
    private TutorialManager tutorialManager;
    private HandController handController;
    public Sprite hoverIcon;
    private PlayerInteraction playerInteraction;

    void Awake()
    {
        tutorialManager = FindObjectOfType<TutorialManager>();
        handController = FindObjectOfType<HandController>();
        playerInteraction = FindObjectOfType<PlayerInteraction>();
    }

    public override bool Interact()
    {
        if (tutorialManager.IsInStep(TutorialStep.TakeBeerToCustomer)) {
            handController.ReleaseMug();
            tutorialManager.ProgressToNextStep();
            playerInteraction.HideHover();
            return true;
        }

        return false;
    }

    public override Sprite GetHoverIcon()
    {
        if (tutorialManager.IsInStep(TutorialStep.TakeBeerToCustomer)) {
            return hoverIcon;
        }
        
        return null;
    }
}
