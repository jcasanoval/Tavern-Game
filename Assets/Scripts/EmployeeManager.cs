using System.Collections;
using System.Collections.Generic;
using System.Data;
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

    public void NotifyCustomerServedByPlayer(Vector3 position)
    {
        print("Customer served by player Notification");
        print(position);
        Vector3 corrector = new Vector3(0,1,0);
        foreach (Vector3 chair in WaitingChairs)
        {
            print(chair);
            print((chair - (position - corrector)).magnitude);
            if((chair - (position - corrector)).magnitude < 0.5f){
                WaitingChairs.Remove(chair);
                print("removed: " + chair);
                return;
            }
        }
        print("He was not waiting for service");
        foreach (Employee employee in Employees)
        {
            if(employee.OnCancelNotify(position)){
                return;
            }
        }
        print("No employee was serving this customer");
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
            return false;
        }
        else if (goldManager.SpendGold(costToBuyDude))
        {
            SummonEmployee();
            return true;
        }
        return false;
    }

    public void StartNight()
    {
        foreach (Employee employee in Employees)
        {
            employee.OnNightNotify();
        }
    }
}
