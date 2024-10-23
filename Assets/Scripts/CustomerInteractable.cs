using UnityEngine;

public class CustomerInteractable : Interactable
{
    private Customer customer;

    private HandController handController;
    private AudioSource placeBeerAudioSource;
    private PlayerInteraction playerInteraction;
    
    void Awake()
    {
        placeBeerAudioSource = GetComponent<AudioSource>();
        playerInteraction = FindObjectOfType<PlayerInteraction>();
    }

    void Start()
    {
        customer = GetComponentInParent<Customer>();
        handController = FindObjectOfType<HandController>();
    }

    public override bool Interact()
    {
        if (!handController.HasFreeHands() && customer.ServeBeer())
        {
            placeBeerAudioSource.Play();
            handController.ReleaseMug();
            return true;
        }
        return false;
    }

    void OnDestroy() {
        playerInteraction.RemoveInteractable(this);
    }
}
