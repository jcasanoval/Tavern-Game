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
    public AudioClip[] nightMusics;
    public AudioClip[] dayMusics;
    private AudioClip DayMusic
    {
        get
        {
            return dayMusics[Random.Range(0, dayMusics.Length)];
        }
    }
    private AudioClip NightMusic
    {
        get
        {
            return nightMusics[Random.Range(0, nightMusics.Length)];
        }
    }

    private Color colorDaySky = new Color(0.5f, 0.8f, 1f);
    private Color colorNightSky = new Color(0.05f, 0.05f, 0.2f);
    private Camera mainCamera;
    private EmployeeManager employeeManager;

    public Transform door;

    void Awake()
    {
        mainCamera = Camera.main;
        mainCamera.backgroundColor = colorDaySky;
        audioSources = GetComponents<AudioSource>();
        backgroundNoiseAudioSource = audioSources[0];
        doorCloseAudioSource = audioSources[1];
        employeeManager = FindObjectOfType<EmployeeManager>();
        isOpen = false;
        SoundManager.BecomeFather(gameObject);
        StartCoroutine(SoundManager.BackgroundMusicPlay(DayMusic));
    }

    public void Open()
    {
        mainCamera.backgroundColor = colorNightSky;
        FindObjectOfType<LightsManager>().SetNightLights();
        backgroundNoiseAudioSource.Play();
        StartCoroutine(SoundManager.BackgroundMusicPlay(NightMusic));
        isOpen = true;
        StartCoroutine(Close());
        employeeManager.StartNight();
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

        FindObjectOfType<LightsManager>().SetDayLights();

        CloseDoor();

        OverSeerObserver.Instance.Notify(OverSeerEvent.NightEnd);
    }

    public void ForceClose()
    {
        StopAllCoroutines();
        isOpen = false;
        CloseDoor();
    }

    private void CloseDoor()
    {
        mainCamera.backgroundColor = colorDaySky;
        door.position = new Vector3(6.02080011f, 0.753099978f, -4.6262002f);
        door.rotation = Quaternion.Euler(0, 0, 0);
        backgroundNoiseAudioSource.Stop();
        StartCoroutine(SoundManager.BackgroundMusicPlay(DayMusic));
        doorCloseAudioSource.Play();
    }
}
