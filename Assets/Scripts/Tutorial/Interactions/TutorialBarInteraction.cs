using UnityEngine;

public class TutorialBarInteraction : IInteractFunctionality
{
    public Sprite hoverIcon;
    private TutorialManager tutorialManager;
    private HandController handController;
    private PlayerInteraction playerInteraction;

    void Awake()
    {
        tutorialManager = FindObjectOfType<TutorialManager>();
        handController = FindObjectOfType<HandController>();
        playerInteraction = FindObjectOfType<PlayerInteraction>();
    }

    public override bool Interact()
    {
        if (tutorialManager.IsInStep(TutorialStep.ExplainMovement)) {
            handController.HoldMug();
            tutorialManager.ProgressToNextStep();

            Interactable interactable = GetComponentInParent<Interactable>();
            playerInteraction.HideHover(interactable);
            return true;
        }

        return false;
    }

    public override Sprite GetHoverIcon()
    {
        if (tutorialManager.IsInStep(TutorialStep.ExplainMovement)) {
            return hoverIcon;
        }

        return null;
    }
}