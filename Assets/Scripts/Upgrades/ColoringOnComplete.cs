using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColoringOnComplete : MonoBehaviour
{

    public Vector3 rotation;
    public Vector3 originalRotation;
    public List<UpgradeType> upgradeTypes = new List<UpgradeType>();
    public List<int> upgradeLevels = new List<int>();


    private void Awake() {
        originalRotation = transform.localRotation.eulerAngles;
        UpgradeHandler.ReadyColorForReset(this);
    }
    public void Start(){
        Func<bool> action1 = delegate(){
            Debug.Log("Checking for completion");
            foreach(UpgradeType type in upgradeTypes){
                if(UpgradeHandler.Instance.GetUpgradeLevel(type) < upgradeLevels[upgradeTypes.IndexOf(type)]){
                    return true;
                }
            }
            StartCoroutine(Complete());
            return false;
        };
        OverSeerObserver.Instance.AddListener(OverSeerEvent.UpgradeStat, action1);
        Debug.Log("Added Listener for ColoringOnComplete");
    }
    public IEnumerator Complete(){
        Debug.Log("Completed");
        float time = 0;
        while (time < 1)
        {
            time += Time.unscaledDeltaTime;
            transform.localRotation = Quaternion.Euler(Vector3.Lerp(originalRotation, rotation, Mathf.Sqrt(time)));
            yield return null;
        }
        transform.localRotation = Quaternion.Euler(rotation);
    }

    public void Restart(){
        transform.localRotation = Quaternion.Euler(originalRotation);
        Start();
    }


}
