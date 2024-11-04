using UnityEngine;

public class CustomerInteractable : Interactable
{
    private Customer customer;
    private AudioSource placeBeerAudioSource;
    
    void Awake()
    {
        placeBeerAudioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        customer = GetComponentInParent<Customer>();
    }

    public override bool Interact()
    {
        return InteractFunctionality.Interact();
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
}
