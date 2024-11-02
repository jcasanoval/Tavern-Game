using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    void Start()
    {
        Interactable[] interactable = FindObjectsOfType<Interactable>();
        for (int i = 0; i < interactable.Length; i++)
        {
            interactable[i].InteractFunctionality = interactable[i].TutorialInteractFunctionality;
        }

        // TODO: Esto se va a borrar, está como ejemplo hasta que se implemente el tutorial
        StartCoroutine(ChangeBarrelInteractions());
    }

    IEnumerator ChangeBarrelInteractions()
    {
        yield return new WaitForSeconds(1);
        Interactable[] interactable = FindObjectsOfType<Interactable>();
        for (int i = 0; i < interactable.Length; i++)
        {
            interactable[i].InteractFunctionality = interactable[i].DefaultInteractFunctionality;
        }
    }
}
