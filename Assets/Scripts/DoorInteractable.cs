using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorInteractable : Interactable
{
    private DayCycleManager dayCycleManager;
    public Transform playerTransform;
    public Transform PlayerSpawnPoint;
    private AudioSource doorOpenAudioSource;

    public ExcaliburInteractable excaliburInteractable;

    public Transform changed;

    void Awake()
    {
        doorOpenAudioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        dayCycleManager = FindObjectOfType<DayCycleManager>();
    }
    public override bool Interact()
    {
        if(excaliburInteractable.IsGrabbed){
            playerTransform.position = PlayerSpawnPoint.position;
            excaliburInteractable.ReturnExcalibur();
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
            return true;
        }
        return false;
    }
}
