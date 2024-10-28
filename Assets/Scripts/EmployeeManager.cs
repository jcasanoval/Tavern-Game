using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeManager : Interactable
{
    public List<Employee> Employees = new List<Employee>();
    public List<Vector3> WaitingChairs = new List<Vector3>();

    public int costToBuyDude = 30;
    public GameObject EmployeePrefab;
    private DayCycleManager dayCycleManager;
    private GoldManager goldManager;

    public void AddEmployee(Employee employee)
    {
        Employees.Add(employee);
    }

    public void RemoveEmployee(Employee employee)
    {
        Employees.Remove(employee);
    }

    public Employee SummonEmployee()
    {
        Employee newGuy = Instantiate(EmployeePrefab, transform.position, Quaternion.Euler(30,0,0)).GetComponent<Employee>();
        AddEmployee(newGuy);
        return newGuy;
    }

    public void NotifyNewCustomer(Vector3 position)
    {
        foreach (Employee employee in Employees)
        {
            if(employee.OnNotify(position)){
                return;
            }
        }

        WaitingChairs.Add(position);
    }

    public void NotifyEmployeeFree(Employee employee)
    {
        if(WaitingChairs.Count == 0){
            return;
        }
        employee.OnNotify(WaitingChairs[0]);
        WaitingChairs.RemoveAt(0);
    }

    public void Start()
    {
        dayCycleManager = FindObjectOfType<DayCycleManager>();
        goldManager = FindObjectOfType<GoldManager>();
    }

    public override bool Interact()
    {
        if (dayCycleManager.IsOpen())
        {
            return true;
        }
        else if (goldManager.SpendGold(costToBuyDude))
        {
            SummonEmployee();
            return true;
        }
        return true;
    }
}
