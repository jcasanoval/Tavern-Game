using UnityEngine;

public class TutorialEmptyInteraction : IInteractFunctionality
{
    public override bool Interact()
    {
        Debug.Log("Interacting with " + gameObject.name);
        return false;
    }
}
