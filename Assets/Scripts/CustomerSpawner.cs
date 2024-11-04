using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject customerPrefab;
    public GameObject spriteHolder;
    public float lowestSpawnInterval = 1f;
    public float highestSpawnInterval = 3f;
    private Transform spawnPoint;
    public RaceHolder raceHolder;
    private DayCycleManager dayCycleManager;
    private TutorialManager tutorialManager;

    void Start()
    {
        dayCycleManager = FindObjectOfType<DayCycleManager>();
        tutorialManager = FindObjectOfType<TutorialManager>();
        GameObject spawnPointObject = GameObject.FindGameObjectWithTag("Respawn");
        if (spawnPointObject != null)
        {
            spawnPoint = spawnPointObject.transform;
            StartCoroutine(SpawnCustomers());
        }
        else
        {
            Debug.LogError("Spawn Point with tag 'Respawn' not found.");
        }
    }

    IEnumerator SpawnCustomers()
    {
        while (true)
        {
            if (!dayCycleManager.IsClosing())
            {
                SpawnCustomer();
                float waitTime = Random.Range(lowestSpawnInterval, highestSpawnInterval);
                var currentPopularity = FindObjectOfType<PopularityManager>().Popularity;
                var waitModifier = 0.5f + (1.5f * (1 - (currentPopularity / 5.0f)));
                waitTime *= waitModifier;
                yield return new WaitForSeconds(waitTime);
            }
            else
            {
                yield return null;
            }
        }
    }

    void SpawnCustomer()
    {
        if (customerPrefab != null && spawnPoint != null)
        {
            GameObject customer = Instantiate(customerPrefab, spawnPoint.position, spawnPoint.rotation);
            customer.GetComponent<Customer>().spriteHolder = raceHolder.GetRandomCustomerSpriteHolder();

            if (tutorialManager.IsInTutorialMode)
            {
                customer.GetComponentInChildren<CustomerInteractable>().InteractFunctionality = customer.GetComponentInChildren<CustomerInteractable>().TutorialInteractFunctionality;
            }
            else
            {
                customer.GetComponentInChildren<CustomerInteractable>().InteractFunctionality = customer.GetComponentInChildren<CustomerInteractable>().DefaultInteractFunctionality;
            }

            customer.GetComponent<Customer>().GoToTheBar();
        }
        else
        {
            Debug.LogError("Customer Prefab or Spawn Point is not set.");
        }
    }

    public Customer SpawnHuman()
    {
        Customer customer = Instantiate(customerPrefab, spawnPoint.position, spawnPoint.rotation).GetComponent<Customer>();
        customer.spriteHolder = raceHolder.customerHolders[2];

        if (tutorialManager.IsInTutorialMode)
        {
            customer.GetComponentInChildren<CustomerInteractable>().InteractFunctionality = customer.GetComponentInChildren<CustomerInteractable>().TutorialInteractFunctionality;
        }
        else
        {
            customer.GetComponentInChildren<CustomerInteractable>().InteractFunctionality = customer.GetComponentInChildren<CustomerInteractable>().DefaultInteractFunctionality;
        }

        customer.spriteRenderer.sprite = customer.spriteHolder.GetProfile();

        return customer;
    }
}
