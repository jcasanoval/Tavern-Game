using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerInteraction : IInteractFunctionality
{
    private Customer customer;
    private CustomerInteractable customerInteractable;

    private HandController handController;
    private AudioSource placeBeerAudioSource;
    private PlayerInteraction playerInteraction;
    private EmployeeManager employeeManager;
    
    void Awake()
    {
        customerInteractable = GetComponentInParent<CustomerInteractable>();
        placeBeerAudioSource = customerInteractable.GetComponent<AudioSource>();
        playerInteraction = FindObjectOfType<PlayerInteraction>();
        employeeManager = FindObjectOfType<EmployeeManager>();
    }

    void Start()
    {
        customer = customerInteractable.GetComponentInParent<Customer>();
        handController = FindObjectOfType<HandController>();
    }

    public override bool Interact()
    {
        if (handController.HasMug() && customer.ServeBeer())
        {
            placeBeerAudioSource.Play();
            handController.ReleaseMug();
            employeeManager.NotifyCustomerServedByPlayer(customer.transform.position - new Vector3(0, 1, 0));
            return true;
        }
        return false;
    }

    void OnDestroy() {
        playerInteraction.RemoveInteractable(customerInteractable);
    }

    public override Sprite GetHoverIcon()
    {
        return null;
    }
}
