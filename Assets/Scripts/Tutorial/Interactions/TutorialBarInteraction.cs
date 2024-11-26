using UnityEngine;

public class TutorialBarInteraction : IInteractFunctionality
{
    public Sprite hoverIcon;
    private TutorialManager tutorialManager;
    private HandController handController;
    private PlayerInteraction playerInteraction;
    private AudioSource serveBeerAudioSource;
    private BarInteractable barInteractable;

    void Awake()
    {
        barInteractable = GetComponentInParent<BarInteractable>();
        serveBeerAudioSource = barInteractable.GetComponent<AudioSource>();
        tutorialManager = FindObjectOfType<TutorialManager>();
        handController = FindObjectOfType<HandController>();
        playerInteraction = FindObjectOfType<PlayerInteraction>();
    }

    public override bool Interact()
    {
        if ((tutorialManager.IsInStep(TutorialStep.GetBeer) || tutorialManager.IsInStep(TutorialStep.GetSecondBeer))
            && barInteractable.Stock > 0) {
            handController.HoldMug();
            FindObjectOfType<BarInteractable>().Stock--;
            serveBeerAudioSource.Play();
            tutorialManager.ProgressToNextStep();

            Interactable interactable = GetComponentInParent<Interactable>();
            playerInteraction.HideHover(interactable);
            return true;
        }

        return false;
    }

    public override Sprite GetHoverIcon()
    {
        if ((tutorialManager.IsInStep(TutorialStep.GetBeer) || tutorialManager.IsInStep(TutorialStep.GetSecondBeer))
            && barInteractable.Stock > 0) {
            return hoverIcon;
        }

        return null;
    }
}