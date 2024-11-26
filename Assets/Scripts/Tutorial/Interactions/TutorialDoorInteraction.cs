using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDoorInteraction : IInteractFunctionality
{
    public Sprite hoverIcon;
    public Transform playerTransform;
    public Transform PlayerSpawnPoint;
    private TutorialManager tutorialManager;
    private PlayerInteraction playerInteraction;
    private DoorInteractable doorInteractable;
    private TutorialExcaliburInteraction excaliburInteraction;
    private DoorInteractable door;
    private AudioSource doorOpenAudioSource;

    void Awake()
    {
        tutorialManager = FindObjectOfType<TutorialManager>();
        playerInteraction = FindObjectOfType<PlayerInteraction>();
        doorInteractable = GetComponentInParent<DoorInteractable>();
        excaliburInteraction = FindObjectOfType<TutorialExcaliburInteraction>();
        door = GetComponentInParent<DoorInteractable>();
        doorOpenAudioSource = door.GetComponents<AudioSource>()[0];
    }

    public override bool Interact()
    {
        if(excaliburInteraction.IsGrabbed){
            playerInteraction.HideHover(door);
            playerTransform.position = PlayerSpawnPoint.position;
            excaliburInteraction.ReturnExcalibur();
            doorOpenAudioSource.Play();
            return true;
        }

        if(playerTransform.transform.position.x > transform.position.x){
            return false;
        }

        if (tutorialManager.IsInStep(TutorialStep.OpenTheBar) || tutorialManager.IsInStep(TutorialStep.OpenTheBarAgain)) {
            doorInteractable.OpenDoor();

            tutorialManager.ProgressToNextStep();

            Interactable interactable = GetComponentInParent<Interactable>();
            playerInteraction.HideHover(interactable);
            
            return true;
        }

        return false;
    }

    public override Sprite GetHoverIcon()
    {
        if (excaliburInteraction.IsGrabbed || (playerTransform.transform.position.x <= transform.position.x && 
            (tutorialManager.IsInStep(TutorialStep.OpenTheBar) || tutorialManager.IsInStep(TutorialStep.OpenTheBarAgain)))) {
            return hoverIcon;
        }

        return null;
    }
}
