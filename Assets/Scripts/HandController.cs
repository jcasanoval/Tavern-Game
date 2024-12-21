using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    [SerializeField] private GameObject mug;
    [SerializeField] private GameObject mug2;
    [SerializeField] private GameObject mug3;

    public int heldMugs = 0;

    
    void Start() {
        mug.SetActive(false);
        mug2.SetActive(false);
        mug3.SetActive(false);
    }

    public bool HasFreeHands() {
        switch(UpgradeHandler.Instance.StrengthLevel) {
            case 0:
                return !mug.activeSelf;
            case 1:
                return !mug2.activeSelf;
            case 2:
                return !mug3.activeSelf;
            default:
                return !mug.activeSelf;
        }
    }

    public bool HasMug() {
        return heldMugs > 0;
    }

    public void HoldMug() {
        switch(heldMugs) {
            case 0:
                mug.SetActive(true);
                break;
            case 1:
                mug2.SetActive(true);
                break;
            case 2:
                mug3.SetActive(true);
                break;
        }
        heldMugs++;
    }

    public void ReleaseMug() {
        switch(heldMugs) {
            case 1:
                mug.SetActive(false);
                break;
            case 2:
                mug2.SetActive(false);
                break;
            case 3:
                mug3.SetActive(false);
                break;
        }
        heldMugs--;
    }

    public void ReleaseAllMugs() {
        mug.SetActive(false);
        mug2.SetActive(false);
        mug3.SetActive(false);
        heldMugs = 0;
    }
}
