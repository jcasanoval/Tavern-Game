using UnityEngine;

public class TutorialEmptyInteraction : IInteractFunctionality
{
    public override bool Interact()
    {
        Debug.Log("Interacting with " + gameObject.name);
        return false;
    }

    public override Sprite GetHoverIcon()
    {
        Debug.Log("Getting hover icon from " + gameObject.name);
        return null;
    }
}
