using UnityEngine;

public class DoorInteractable : Interactable
{    
    private AudioSource doorOpenAudioSource;
    private AudioSource doorCloseAudioSource;
    public Transform door;

    void Awake()
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        doorOpenAudioSource = audioSources[0];
        doorCloseAudioSource = audioSources[1];
    }

    public void OpenDoor()
    {
        doorOpenAudioSource.Play();
        door.position = new Vector3(6.79f,0.753099978f,-5.5f);
        door.rotation = Quaternion.Euler(0, 90, 0);
    }

    public void CloseDoor()
    {
        door.position = new Vector3(6.02080011f,0.753099978f,-4.6262002f);
        door.rotation = Quaternion.Euler(0, 0, 0);
        doorCloseAudioSource.Play();
    }

    public override bool Interact()
    {
        return InteractFunctionality.Interact();
    }
    public override Sprite GetHoverIcon()
    {
        return InteractFunctionality.GetHoverIcon();
    }
}
