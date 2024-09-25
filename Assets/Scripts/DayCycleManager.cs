using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCycleManager : MonoBehaviour
{
    public float nightTime = 20f;
    private bool isOpen;
    private AudioSource backgroundNoiseAudioSource;

    void Awake()
    {
        backgroundNoiseAudioSource = GetComponent<AudioSource>();
        isOpen = false;
    }

    public void Open()
    {
        backgroundNoiseAudioSource.Play();
        isOpen = true;
        StartCoroutine(Close());
    }

    public bool IsOpen()
    {
        bool customersExist = FindObjectsOfType<Customer>().Length > 0;

        return isOpen || customersExist;
    }

    public bool IsClosing()
    {
        return !isOpen;
    }

    IEnumerator Close()
    {
        yield return new WaitForSeconds(nightTime);

        isOpen = false;

        while (IsOpen())
        {
            yield return null;
        }

        backgroundNoiseAudioSource.Stop();
    }
}
