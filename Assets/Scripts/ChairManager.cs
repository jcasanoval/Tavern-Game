using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChairManager : MonoBehaviour
{
    private List<Chair> chairs = new List<Chair>();

    void Start()
    {
        chairs = FindObjectsOfType<Chair>().ToList();
        Debug.Log("Found " + chairs.Count + " chairs.");
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
