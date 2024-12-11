using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsManager : MonoBehaviour
{
    [SerializeField]
    private GameObject dayLight;
    [SerializeField]
    private GameObject nightLight;

    public void SetDayLights()
    {
        dayLight.SetActive(true);
        nightLight.SetActive(false);
    }

    public void SetNightLights()
    {
        dayLight.SetActive(false);
        nightLight.SetActive(true);
    }
}
