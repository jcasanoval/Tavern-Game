using UnityEngine;

public class CustomerInteractable : Interactable
{
    private Customer customer;

    private HandController handController;

    void Start()
    {
        customer = GetComponentInParent<Customer>();
        handController = FindObjectOfType<HandController>();
    }

    public override void Interact()
    {
        if (!handController.HasFreeHands() && customer.ServeBeer())
        {
            handController.ReleaseMug();
        }
    }
}
