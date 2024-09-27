using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorInteractable : Interactable
{
    private DayCycleManager dayCycleManager;
    private AudioSource doorOpenAudioSource;

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

        if (!dayCycleManager.IsOpen()) {
            dayCycleManager.Open();
            doorOpenAudioSource.Play();
            changed.position = new Vector3(5.25f,0.753099978f,-5.5f);
            changed.rotation = Quaternion.Euler(0, -90, 0);
            return true;
        }
        return false;
    }
}
