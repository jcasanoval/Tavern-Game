using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesInteraction : Interactable
{
    public Sprite hoverIcon;
    TutorialManager tutorialManager;

    void Start()
    {
        tutorialManager = FindObjectOfType<TutorialManager>();
    }

    public override bool Interact()
    {
        if (!tutorialManager.IsInTutorialMode)
        {
            FindObjectOfType<MenuCamera>().CameraState = CameraState.Upgrades;
        }
        return false;
    }

    public override Sprite GetHoverIcon()
    {
        if (!tutorialManager.IsInTutorialMode)
        {
            return hoverIcon;
        }
        return null;
    }
}
