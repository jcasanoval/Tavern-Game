using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractable : Interactable
{
    private DayCycleManager dayCycleManager;

    void Start()
    {
        dayCycleManager = FindObjectOfType<DayCycleManager>();
    }

    void Update() {
        if (dayCycleManager.IsClosing()) {
            GetComponentsInParent<Renderer>()[0].material.color = Color.red;
        }
        else {
            GetComponentsInParent<Renderer>()[0].material.color = Color.green;
        }
    }

    public override void Interact()
    {
        if (!dayCycleManager.IsOpen()) {
            dayCycleManager.Open();
        }
    }
}
