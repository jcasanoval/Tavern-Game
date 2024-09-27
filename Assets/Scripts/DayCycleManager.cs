using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCycleManager : MonoBehaviour
{
    public float nightTime = 20f;
    private bool isOpen;
    private AudioSource backgroundNoiseAudioSource;
    private AudioSource doorCloseAudioSource;
    private AudioSource[] audioSources;

    public Transform door;

    void Awake()
    {
        audioSources = GetComponents<AudioSource>();
        backgroundNoiseAudioSource = audioSources[0];
        doorCloseAudioSource = audioSources[1];
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


        door.position = new Vector3(6.02080011f,0.753099978f,-4.6262002f);
        door.rotation = Quaternion.Euler(0, 0, 0);
        backgroundNoiseAudioSource.Stop();
        doorCloseAudioSource.Play();
    }
}
