public class TutorialBarInteraction : IInteractFunctionality
{
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
}