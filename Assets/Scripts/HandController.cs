using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    [SerializeField] private GameObject mug;
    
    void Start() {
        mug.SetActive(false);
    }

    public bool HasFreeHands() {
        return !mug.activeSelf;
    }

    public void HoldMug() {
        mug.SetActive(true);
    }

    public void ReleaseMug() {
        mug.SetActive(false);
    }
}
