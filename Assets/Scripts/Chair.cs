using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBehaviour
{
    private bool isOccupied = false;
    private GameObject currentCustomer;

    public void AssignCustomer(GameObject customer)
    {
        isOccupied = true;
        currentCustomer = customer;
    }

    public void FreeChair()
    {
        isOccupied = false;
        currentCustomer = null;
    }

    public bool IsOccupied()
    {
        return isOccupied;
    }

    public bool IsOccupiedBy(GameObject customer)
    {
        return isOccupied && currentCustomer == customer;
    }
}
