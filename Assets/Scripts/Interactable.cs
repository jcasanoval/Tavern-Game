using UnityEngine;

public class Interactable : MonoBehaviour
{
    public virtual bool Interact()
    {
        Debug.Log("Interacting with " + gameObject.name);
        return false;
    }

    public virtual bool NPCInteract(GameObject npc)
    {
        Debug.Log("NPC Interacting with " + gameObject.name);
        return false;
    }
}
