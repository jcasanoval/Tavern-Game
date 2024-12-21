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
        other.GetComponentInParent<Animator>().SetTrigger("Falls");
        handController.ReleaseAllMugs();
        OverSeerObserver.Instance.Notify(OverSeerEvent.Fall_Off_Table);
    }
}
