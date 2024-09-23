using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChairManager : MonoBehaviour
{
    private List<Chair> chairs = new List<Chair>();

    void Start()
    {
        RefreshChairs();
        Debug.Log("Found " + chairs.Count + " chairs.");
    }

    public void RefreshChairs()
    {
        chairs = FindObjectsOfType<Chair>().ToList();
    }

    public Vector3? GetAvailableChairPosition(GameObject customer)
    {
        foreach (Chair chair in chairs)
        {
            if (!chair.IsReserved())
            {
                chair.AssignCustomer(customer);
                return chair.transform.position;
            }
        }
        return null;
    }

    public void FreeChairForCustomer(GameObject customer)
    {
        GetChairByCustomer(customer)?.FreeChair();
    }

    public Chair GetChairByCustomer(GameObject customer)
    {
        foreach (Chair chair in chairs)
        {
            if (chair.IsReservedBy(customer))
            {
                return chair;
            }
        }
        return null;
    }
}
