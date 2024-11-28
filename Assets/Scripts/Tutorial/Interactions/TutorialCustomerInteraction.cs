using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TutorialCustomerInteraction : IInteractFunctionality
{
    private TutorialManager tutorialManager;
    private HandController handController;
    public Sprite hoverIcon;
    private PlayerInteraction playerInteraction;
    private Customer customer;
    private CustomerInteractable customerInteractable;

    void Awake()
    {
        tutorialManager = FindObjectOfType<TutorialManager>();
        handController = FindObjectOfType<HandController>();
        playerInteraction = FindObjectOfType<PlayerInteraction>();
        customerInteractable = GetComponentInParent<CustomerInteractable>();
    }

    void Start()
    {
        customer = customerInteractable.GetComponentInParent<Customer>();
    }

    public override bool Interact()
    {
        if (tutorialManager.IsInStep(TutorialStep.TakeBeerToCustomer)
        || tutorialManager.IsInStep(TutorialStep.TakeBeerToSecondCustomer)) {
            customer.ServeBeer();
            handController.ReleaseMug();
            playerInteraction.HideHover();
            tutorialManager.ProgressToNextStep();
            return true;
        }

        return false;
    }

    public override Sprite GetHoverIcon()
    {
        if ((tutorialManager.IsInStep(TutorialStep.TakeBeerToCustomer)
        || tutorialManager.IsInStep(TutorialStep.TakeBeerToSecondCustomer))
        && customer.isSitting) {
            return hoverIcon;
        }
        
        return null;
    }
}
