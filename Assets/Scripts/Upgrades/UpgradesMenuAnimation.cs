using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesMenuAnimation : MonoBehaviour
{
    public Vector3 displayPosition;
    public Vector3 displayRotation;

    public Vector3 hidePosition;
    public Vector3 hideRotation;

    private bool isDisplayed = false;

    public bool IsDisplayed
    {
        get { return isDisplayed; }
        set
        {
            isDisplayed = value;
            if (isDisplayed)
            {
                transform.position = displayPosition;
                transform.rotation = Quaternion.Euler(displayRotation);
            }
            else
            {
                transform.position = hidePosition;
                transform.rotation = Quaternion.Euler(hideRotation);
            }
            gameObject.SetActive(false);
            gameObject.SetActive(true);
        }
    }
}
