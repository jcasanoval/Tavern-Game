using UnityEngine;

public class Interactable : MonoBehaviour
{
    public virtual bool Interact()
    {
        Debug.Log("Interacting with " + gameObject.name);
        return false;
    }
}
