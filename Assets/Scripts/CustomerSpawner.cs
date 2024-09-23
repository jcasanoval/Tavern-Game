using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject customerPrefab;
    public GameObject spriteHolder;
    public float spawnInterval = 5f;
    private Transform spawnPoint;
    public RaceHolder raceHolder;
    private DayCycleManager dayCycleManager;

    void Start()
    {
        dayCycleManager = FindObjectOfType<DayCycleManager>();
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
                yield return new WaitForSeconds(spawnInterval);
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
            customer.GetComponent<Customer>().spriteHolder = raceHolder.GetRandomSpriteHolder();
        }
        else
        {
            Debug.LogError("Customer Prefab or Spawn Point is not set.");
        }
    }

}
