using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    void Start()
    {
        // this is just a test
        // it changes the interaction of all barrels to the tutorial interaction for 1 second
        Barrel[] barrels = FindObjectsOfType<Barrel>();
        Debug.Log("Barrels: " + barrels.Length);
        for (int i = 0; i < barrels.Length; i++)
        {
            barrels[i].InteractFunctionality = barrels[i].TutorialInteractFunctionality;
        }

        // wait 1 sec
        StartCoroutine(ChangeBarrelInteractions());
    }

    IEnumerator ChangeBarrelInteractions()
    {
        yield return new WaitForSeconds(1);
        Barrel[] barrels = FindObjectsOfType<Barrel>();
        for (int i = 0; i < barrels.Length; i++)
        {
            barrels[i].InteractFunctionality = barrels[i].DefaultInteractFunctionality;
        }
    }
}
