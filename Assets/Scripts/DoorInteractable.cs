using UnityEngine;

public class DoorInteractable : Interactable
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
