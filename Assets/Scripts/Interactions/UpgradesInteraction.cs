using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesInteraction : IInteractFunctionality
{
    public Sprite hoverIcon;

    public override bool Interact()
    {

        if (CanInteract())
        {
            FindObjectOfType<MenuCamera>().CameraState = CameraState.Upgrades;

            return true;
        }

        return false;
    }

    public override Sprite GetHoverIcon()
    {
        if (CanInteract())
        {
            return hoverIcon;
        }
        return null;
    }

    private bool CanInteract()
    {
        var isOpen = FindObjectOfType<DayCycleManager>().IsOpen();
        var isTutoarial = FindObjectOfType<TutorialManager>().IsInTutorialMode;
        return !isOpen && !isTutoarial;
    }
}
