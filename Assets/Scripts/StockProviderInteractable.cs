using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockProviderInteractable : Interactable
{
    public override bool Interact()
    {
        return InteractFunctionality.Interact();
    }
}
