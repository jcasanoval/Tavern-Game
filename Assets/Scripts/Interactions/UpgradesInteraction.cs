using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesInteraction : IInteractFunctionality
{
    public Sprite hoverIcon;

    public override bool Interact()
    {
        FindObjectOfType<MenuCamera>().CameraState = CameraState.Upgrades;
        
        return true;
    }

    public override Sprite GetHoverIcon()
    {
        return hoverIcon;
    }
}
