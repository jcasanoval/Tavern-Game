using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairManager : MonoBehaviour
{
    public Transform chairParent;
    private List<Chair> chairs = new List<Chair>();

    void Start() {
        FindAllChairs();
        Debug.Log("Found " + chairs.Count + " chairs.");
    }

    void FindAllChairs()
    {
        if (chairParent != null)
        {
            chairs.AddRange(chairParent.GetComponentsInChildren<Chair>());
        }
        else
        {
            Debug.LogError("Chair parent not assigned!");
        }
    }

    public Vector3? GetAvailableChairPosition(GameObject customer)
    {
        foreach (Chair chair in chairs)
        {
            if (!chair.IsOccupied())
            {
                chair.AssignCustomer(customer);
                return chair.transform.position;
            }
        }
        return null;
    }

    public void FreeChairForCustomer(GameObject customer)
    {
        foreach (Chair chair in chairs)
        {
            if (chair.IsOccupiedBy(customer))
            {
                chair.FreeChair();
                return;
            }
        }
    }

}
