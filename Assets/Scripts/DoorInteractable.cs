using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractable : Interactable
{
    private DayCycleManager dayCycleManager;
    private AudioSource doorOpenAudioSource;

    void Awake()
    {
        doorOpenAudioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        dayCycleManager = FindObjectOfType<DayCycleManager>();
    }

    void Update() {
        if (dayCycleManager.IsOpen()) {
            GetComponentsInParent<Renderer>()[0].material.color = Color.green;
        }
        else {
            GetComponentsInParent<Renderer>()[0].material.color = Color.red;
        }
    }

    public override bool Interact()
    {
        if (!dayCycleManager.IsOpen()) {
            dayCycleManager.Open();
            doorOpenAudioSource.Play();
            return true;
        }
        return false;
    }
}
