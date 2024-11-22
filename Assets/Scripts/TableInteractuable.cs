using UnityEngine;

public class TableInteractuable : Interactable
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
