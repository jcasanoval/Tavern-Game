using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCustomerInteraction : IInteractFunctionality
{
    private TutorialManager tutorialManager;
    private HandController handController;

    void Awake()
    {
        tutorialManager = FindObjectOfType<TutorialManager>();
        handController = FindObjectOfType<HandController>();
    }

    public override bool Interact()
    {
        if (tutorialManager.IsInStep(TutorialStep.TakeBeerToCustomer)) {
            handController.ReleaseMug();
            tutorialManager.ProgressToNextStep();
            return true;
        }

        return false;
    }
}
