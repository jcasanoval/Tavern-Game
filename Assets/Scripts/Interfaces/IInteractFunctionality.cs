using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IInteractFunctionality : MonoBehaviour
{
    public virtual bool Interact()
    {
        Debug.Log("Interacting with " + gameObject.name);
        return false;
    }
}
