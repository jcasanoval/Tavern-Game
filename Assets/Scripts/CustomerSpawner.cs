using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject customerPrefab;
    public float spawnInterval = 5f;
    private Transform spawnPoint;

    void Start()
    {
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
            SpawnCustomer();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnCustomer()
    {
        if (customerPrefab != null && spawnPoint != null)
        {
            Instantiate(customerPrefab, spawnPoint.position, spawnPoint.rotation);
        }
        else
        {
            Debug.LogError("Customer Prefab or Spawn Point is not set.");
        }
    }

}
