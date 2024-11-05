using UnityEngine;

public class IInteractFunctionality : MonoBehaviour
{
    public virtual bool Interact()
    {
        Debug.Log("Interacting with " + gameObject.name);
        return false;
    }

    public virtual bool NPCInteract(GameObject npc)
    {
        Debug.Log("NPC " + npc.name + "Interacting with " + gameObject.name);
        return false;
    }
}
