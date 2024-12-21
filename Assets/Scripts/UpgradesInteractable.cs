using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesInteractable : Interactable
{
    public override bool Interact()
    {
        return InteractFunctionality.Interact();
    }
    
    public override Sprite GetHoverIcon()
    {
        return InteractFunctionality.GetHoverIcon();
    }
}
