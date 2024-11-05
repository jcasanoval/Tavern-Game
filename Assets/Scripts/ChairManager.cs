using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChairManager : MonoBehaviour
{
    private List<Chair> chairs = new List<Chair>();
    private EmployeeManager employeeManager;

    void Start()
    {
        RefreshChairs();
        Debug.Log("Found " + chairs.Count + " chairs.");
        employeeManager = FindObjectOfType<EmployeeManager>();
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

    public void SitOnChair(Chair chair){
        employeeManager.NotifyNewCustomer(chair.transform.position);
    }

    public void AngrilyLeaveChair(GameObject customer)
    {
        Chair chair = GetChairByCustomer(customer);
        
        employeeManager.NotifyCustomerServedByPlayer(chair.transform.position);

    }
}
