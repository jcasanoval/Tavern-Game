using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExcaliburInteractable : Interactable
{
    private DayCycleManager dayCycleManager;

    void Awake() 
    {
        dayCycleManager = FindObjectOfType<DayCycleManager>();
    }

    public override bool Interact()
    {
        return InteractFunctionality.Interact();
    }
    
    public override Sprite GetHoverIcon()
    {
        return InteractFunctionality.GetHoverIcon();
    }
}
