using UnityEngine;

public class Chair : MonoBehaviour
{
    private bool isReserved = false;
    private GameObject currentCustomer;

    public void AssignCustomer(GameObject customer)
    {
        isReserved = true;
        currentCustomer = customer;
    }

    public void FreeChair()
    {
        isReserved = false;
        currentCustomer = null;
    }

    public bool IsReserved()
    {
        return isReserved;
    }

    public bool IsReservedBy(GameObject customer)
    {
        return isReserved && currentCustomer == customer;
    }
}
