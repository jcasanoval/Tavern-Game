using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExcaliburInteractable : Interactable
{
    public override bool Interact(){
        return InteractFunctionality.Interact();
    }
}
