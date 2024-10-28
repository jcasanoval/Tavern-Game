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

    public override bool NPCInteract(GameObject npc)
    {
        print("NPC " + npc.name + "Interacting with " + gameObject.name);
        if(npc.tag != "Employee"){
            return false;
        }
        Employee employee = npc.GetComponent<Employee>();

        if (employee.HasBeer && customer.ServeBeer())
        {
            placeBeerAudioSource.Play();
            employee.HasBeer = false;
            return true;
        }
        return false;
    }

    void OnDestroy() {
        playerInteraction.RemoveInteractable(this);
    }
}
