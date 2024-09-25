using UnityEngine;

public class CustomerInteractable : Interactable
{
    private Customer customer;

    private HandController handController;
    private AudioSource placeBeerAudioSource;
    
    void Awake()
    {
        placeBeerAudioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        customer = GetComponentInParent<Customer>();
        handController = FindObjectOfType<HandController>();
    }

    public override void Interact()
    {
        if (!handController.HasFreeHands() && customer.ServeBeer())
        {
            placeBeerAudioSource.Play();
            handController.ReleaseMug();
        }
    }
}
