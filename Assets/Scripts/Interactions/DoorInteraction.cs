using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteraction : IInteractFunctionality
{
    public DayCycleManager dayCycleManager;
    public Transform playerTransform;
    public Transform PlayerSpawnPoint;
    public Sprite hoverIcon;
    private AudioSource doorOpenAudioSource;
    private DoorInteractable door;
    private PlayerInteraction playerInteraction;

    public ExcaliburInteraction excaliburInteraction;

    public Transform changed;

    void Awake()
    {
        door = GetComponentInParent<DoorInteractable>();
        doorOpenAudioSource = door.GetComponents<AudioSource>()[0];
        playerInteraction = FindObjectOfType<PlayerInteraction>();
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
        
        if (!dayCycleManager.IsOpen()) {
            dayCycleManager.Open();
            doorOpenAudioSource.Play();
            changed.position = new Vector3(6.79f,0.753099978f,-5.5f);
            changed.rotation = Quaternion.Euler(0, 90, 0);
            playerInteraction.HideHover(door);
            return true;
        }
        return false;
    }

    public override Sprite GetHoverIcon()
    {
        if (excaliburInteraction.IsGrabbed || (playerTransform.transform.position.x <= transform.position.x && !dayCycleManager.IsOpen())) {
            return hoverIcon;
        }
        
        return null;
    }
}
