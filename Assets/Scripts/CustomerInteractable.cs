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
            // TODO: BORRAR LAs SIGUIENTES LINEAS, SON LAS QUE DEJAN DE CABEZA AL JUGADOR
            Vector3 currentRotation = customer.transform.eulerAngles;
            customer.transform.eulerAngles = new Vector3(currentRotation.x, currentRotation.y + 180f, currentRotation.z);
        }
    }
}
