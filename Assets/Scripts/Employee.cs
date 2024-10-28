using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Employee : MonoBehaviour
{
    private bool _isBusy = false;
    private NavMeshAgent agent;
    private Vector3 targetPosition;

    private EmployeeManager _employeeManager;
    private EmployeeInteraction _interaction;

    public GameObject Mug;

    private bool _hasBeer = false;

    public bool HasBeer
    {
        get
        {
            return _hasBeer;
        }

        set
        {
            _hasBeer = value;
            Mug.SetActive(_hasBeer);
        }
    }

    public bool IsBusy
    {
        get
        {
            return _isBusy;
        }

        set{
            _isBusy = value;
            if(!_isBusy){
                _employeeManager.NotifyEmployeeFree(this);
            }
        }
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        _employeeManager = FindObjectOfType<EmployeeManager>();
        _interaction = GetComponentInChildren<EmployeeInteraction>();
        StartCoroutine(GrabABeer());

    }
    
    public bool OnNotify(Vector3 position)
    {
        if(IsBusy){
            return false;
        }
        targetPosition = position;
        agent.SetDestination(targetPosition);
        StartCoroutine(ServeACustomer());

        Debug.Log("Employee notified at " + position);
        return true;
    }

    IEnumerator GrabABeer()
    {
        IsBusy = true;
        targetPosition = GetNearestBarrel();
        agent.SetDestination(targetPosition);
        while (agent.pathPending || agent.remainingDistance > 0.5f)
        {
            yield return null;
        }
        print("Grabbing a beer");
        _interaction.TryToInteract();
        IsBusy = false;
    }

    IEnumerator ServeACustomer()
    {
        IsBusy = true;
        if(HasBeer){
            while (agent.pathPending || agent.remainingDistance > 0.5f)
            {
                yield return null;
            }
            _interaction.TryToInteract();
        }
        StartCoroutine(GrabABeer());
    }

    public Vector3 GetNearestBarrel(){
        return FindObjectOfType<Barrel>().transform.position;
    }

    public void GotBeer(){
        HasBeer = true;
    }

    public void ServeBeer(){
        HasBeer = false;
    }

}
