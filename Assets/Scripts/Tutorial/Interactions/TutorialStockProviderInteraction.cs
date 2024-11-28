using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialStockProviderInteraction : IInteractFunctionality
{
    public Sprite hoverIcon;
    private TutorialManager tutorialManager;
    private PlayerInteraction playerInteraction;
    public int beerCost = 1;
    private GoldManager goldManager;

    void Awake()
    {
        tutorialManager = FindObjectOfType<TutorialManager>();
        playerInteraction = FindObjectOfType<PlayerInteraction>();
        goldManager = FindObjectOfType<GoldManager>();
    }

    public override bool Interact()
    {
        if (tutorialManager.IsInStep(TutorialStep.BuyABeer) && goldManager.SpendGold(beerCost)) {
            Interactable interactable = GetComponentInParent<Interactable>();
            playerInteraction.HideHover(interactable);

            FindObjectOfType<BarInteractable>().Stock++;
            tutorialManager.ProgressToNextStep();
            
            return true;
        }

        if (tutorialManager.IsInStep(TutorialStep.OpenTheBarAgain) && goldManager.SpendGold(beerCost)) {
            if (!goldManager.CanSpendGold(beerCost)) {
                Interactable interactable = GetComponentInParent<Interactable>();
                playerInteraction.HideHover(interactable);
            }

            FindObjectOfType<BarInteractable>().Stock++;
            
            return true;
        }

        return false;
    }

    public override Sprite GetHoverIcon()
    {
        if (tutorialManager.IsInStep(TutorialStep.BuyABeer)) {
            return hoverIcon;
        }

        if (tutorialManager.IsInStep(TutorialStep.OpenTheBarAgain) && goldManager.CanSpendGold(beerCost)) {
            return hoverIcon;
        }

        return null;
    }
}
