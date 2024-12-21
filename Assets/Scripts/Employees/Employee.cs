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
    public Vector3 restingPosition;
    public Vector3 RestingPosition
    {
        get
        {
            return new Vector3(restingPosition.x + Random.Range(-.3f,.3f), restingPosition.y, restingPosition.z);
        }
    }

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
        if(UpgradeHandler.Instance.IntelligenceLevel >= 1){
            agent.speed *= 2;
        }
    }
    
    public bool OnNotify(Vector3 position)
    {
        if(IsBusy){
            return false;
        }
        targetPosition = position;
        agent.SetDestination(targetPosition);
        if(_hasBeer){
            StartCoroutine(ServeACustomer());
        }
        else{
            StartCoroutine(GrabABeer());
            return false;
        }

        Debug.Log("Employee notified at " + position);
        return true;
    }

    public bool OnCancelNotify(Vector3 position)
    {
        if((targetPosition - (position - new Vector3(0,0,0))).magnitude < 0.5f || (targetPosition == GetNearestBarInteractable() && ((Vector2)transform.position - (Vector2)position).magnitude < 0.5f)){
            agent.ResetPath();
            targetPosition = GetNearestBarInteractable();
            IsBusy = false;
            return true;
        }
        return false;
    }

    IEnumerator GrabABeer()
    {
        IsBusy = true;
        targetPosition = GetNearestBarInteractable();
        agent.SetDestination(targetPosition);
        while (agent.pathPending || agent.remainingDistance > 0.5f)
        {
            if(!IsBusy){
                yield break;
            }
            yield return null;
        }
        print("Grabbing a beer");
        _interaction.TryToInteract();
        agent.SetDestination(RestingPosition);
        IsBusy = false;
    }

    IEnumerator ServeACustomer()
    {
        IsBusy = true;
        if(HasBeer){
            while (agent.pathPending || agent.remainingDistance > 0.5f)
            {
                if(!IsBusy){
                    yield break;
                }
                yield return null;
            }
            _interaction.TryToInteract();
        }
        StartCoroutine(GrabABeer());
    }

    public Vector3 GetNearestBarInteractable(){
        //TODO: Implement this
        Vector3 nearestBarInteractable = RestingPosition;
        float minDistance = Mathf.Infinity;
        foreach (BarInteractable barInteractable in FindObjectsOfType<BarInteractable>())
        {
            float distance = (barInteractable.transform.position - transform.position).magnitude;
            if(distance < minDistance && barInteractable.Stock > 0){
                minDistance = distance;
                nearestBarInteractable = barInteractable.transform.position;
            }
        }
        return nearestBarInteractable;
    }

    public void OnUpgradeSpeed(){
        agent.speed *= 2;
    }

    public void GotBeer(){
        HasBeer = true;
    }

    public void ServeBeer(){
        HasBeer = false;
    }

    public void OnNightNotify(){
        if(!HasBeer){
            StartCoroutine(GrabABeer());
        }
    }

}
