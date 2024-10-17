using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingFromTheWorld : MonoBehaviour
{
    private HandController handController;

    void Start()
    {
        handController = FindObjectOfType<HandController>();
    }

    private void OnTriggerEnter(Collider other) {
        other.GetComponent<Animator>().SetTrigger("Falls");
        handController.ReleaseMug();
    }
}
