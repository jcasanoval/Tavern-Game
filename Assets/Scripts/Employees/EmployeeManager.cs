
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.Collections;
using UnityEngine;

public class EmployeeManager : Interactable
{
    public List<Employee> Employees = new List<Employee>();
    public List<Vector3> WaitingChairs = new List<Vector3>();

    public int costToBuyDude = 30;
    public GameObject EmployeePrefab;

    public Vector3 restArea1;
    public Vector3 restArea2;
    private Vector3 _lastRestPlace;

    private Vector3 NextRestPlace
    {
        get
        {
            Vector3 rest = new Vector3(_lastRestPlace.x + 1f, _lastRestPlace.y, _lastRestPlace.z);
            if (rest.x > restArea2.x)
            {
                rest = new Vector3(restArea1.x, rest.y, rest.z - .5f);
                if (rest.z < restArea2.z)
                {
                    rest = new Vector3(restArea1.x, rest.y, restArea1.z);
                }
            }
            _lastRestPlace = rest;
            return rest;
        }
    }

    public void AddEmployee(Employee employee)
    {
        Employees.Add(employee);
    }


    public void RemoveAllEmployees()
    {
        foreach (Employee employee in Employees)
        {
            Destroy(employee.gameObject);
        }
        Employees.Clear();
    }

    public Employee SummonEmployee()
    {
        Employee newGuy = Instantiate(EmployeePrefab, transform.position, Quaternion.Euler(30, 0, 0)).GetComponent<Employee>();
        Vector3 newGuyPosition = NextRestPlace;
        //newGuyPosition = new Vector3(newGuyPosition.x + Random.Range(-.2f,.2f), newGuyPosition.y, newGuyPosition.z);
        newGuy.restingPosition = newGuyPosition;
        AddEmployee(newGuy);
        return newGuy;
    }

    public void NotifyNewCustomer(Vector3 position)
    {
        foreach (Employee employee in Employees)
        {
            if (employee.OnNotify(position))
            {
                return;
            }
        }

        WaitingChairs.Add(position);
    }

    public void NotifyCustomerServedByPlayer(Vector3 position)
    {
        print("Customer served by player Notification");
        print(position);
        Vector3 corrector = new Vector3(0, 0, 0);
        foreach (Vector3 chair in WaitingChairs)
        {
            print(chair);
            print((chair - (position - corrector)).magnitude);
            if ((chair - (position - corrector)).magnitude < 0.5f)
            {
                WaitingChairs.Remove(chair);
                print("removed: " + chair);
                break;
            }
        }
        print("He was not waiting for service");
        foreach (Employee employee in Employees)
        {
            if (employee.OnCancelNotify(position))
            {
                return;
            }
        }
        print("No employee was serving this customer");
    }



    public void NotifyEmployeeFree(Employee employee)
    {
        if (WaitingChairs.Count == 0)
        {
            return;
        }
        employee.OnNotify(WaitingChairs[0]);
        WaitingChairs.RemoveAt(0);
    }

    public void Start()
    {
        _lastRestPlace = restArea1;
        _lastRestPlace = NextRestPlace;
    }

    public override bool Interact()
    {
        return InteractFunctionality.Interact();
    }

    public void StartNight()
    {
        foreach (Employee employee in Employees)
        {
            employee.OnNightNotify();
        }
    }

    public override Sprite GetHoverIcon()
    {
        return InteractFunctionality.GetHoverIcon();
    }

    public void OnDrawGizmos()
    {
        if (restArea1 != null && restArea2 != null)
        {
            Gizmos.color = Color.green;
            Vector3 center = (restArea1 + restArea2) / 2;
            Vector3 size = new Vector3(Mathf.Abs(restArea2.x - restArea1.x), 1, Mathf.Abs(restArea2.z - restArea1.z));
            Gizmos.DrawWireCube(center, size);
        }
    }
}
