using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExcaliburInteractable : Interactable
{
    public Sprite hoverIcon;
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
        if (!dayCycleManager.IsOpen())
        {
            return hoverIcon;
        }

        return null;
    }
}
