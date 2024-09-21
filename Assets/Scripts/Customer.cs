using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform exit;
    private ChairManager chairManager;

    public SpriteRenderer spriteRenderer;
    public SpriteHolder spriteHolder;

    private bool isServed = false;
    private bool isSitting = false;

    void Start()
    {
        chairManager = FindObjectOfType<ChairManager>();
        agent = GetComponent<NavMeshAgent>();
        spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.sprite = spriteHolder.GetRandomSprite();

        GameObject exitObject = GameObject.FindGameObjectWithTag("Finish");
        if (exitObject != null)
        {
            exit = exitObject.transform;
            MoveToChair();
        }
        else
        {
            Debug.LogError("Exit with tag 'Finish' not found.");
        }
    }

    void MoveToChair()
    {
        Vector3? chairPosition = chairManager.GetAvailableChairPosition(this.gameObject);

        if (chairPosition.HasValue)
        {
            agent.destination = chairPosition.Value;
            StartCoroutine(WaitForBeerOrLeave());
        }
        else
        {
            Exit();
        }
    }

    IEnumerator WaitForBeerOrLeave()
    {
        while (agent.pathPending || agent.remainingDistance > 0.5f)
        {
            yield return null;
        }

        isSitting = true;

        float waitTime = Random.Range(10f, 20f);
        float elapsedTime = 0f;

        while (elapsedTime < waitTime && !isServed)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        if (!isServed)
        {
            Debug.Log("Customer is leaving because they were not served.");
        }
        else
        {
            Debug.Log("Customer is drinking the beer.");
            yield return new WaitForSeconds(10f);

            FindAnyObjectByType<GoldManager>().AddGold(1);
        }

        isSitting = false;

        chairManager.FreeChairForCustomer(this.gameObject);
        transform.localScale = new Vector3(-transform.localScale.x,transform.localScale.y,transform.localScale.z);
        Exit();
    }

    public bool ServeBeer()
    {
        if (!isServed && isSitting)
        {
            isServed = true;
            Debug.Log("Customer has been served a beer.");
            return true;
        }
        return false;
    }

    void Exit()
    {
        if (exit.transform != null)
        {
            agent.destination = exit.transform.position;

            StartCoroutine(DestroyAfterReachedExit());
        }
    }

    IEnumerator DestroyAfterReachedExit()
    {
        while (agent.pathPending || agent.remainingDistance > 0.5f)
        {
            yield return null;
        }

        Destroy(gameObject);
    }
}
