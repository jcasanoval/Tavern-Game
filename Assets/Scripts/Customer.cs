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

        Debug.Log("Customer " + this.gameObject.name + " is moving to chair " + chairPosition);

        if (chairPosition.HasValue)
        {
            agent.destination = chairPosition.Value;
            StartCoroutine(WaitAndMoveToExit());
        }
        else
        {
            Exit();
        }
    }

    IEnumerator WaitAndMoveToExit()
    {
        while (agent.pathPending || agent.remainingDistance > 0.5f)
        {
            yield return null;
        }

        yield return new WaitForSeconds(5f);

        FindAnyObjectByType<GoldManager>().AddGold(1);

        chairManager.FreeChairForCustomer(this.gameObject);
        transform.localScale = new Vector3(-transform.localScale.x,transform.localScale.y,transform.localScale.z);
        Exit();
    }

    void Exit()
    {
        if (exit.transform != null)
        {
            agent.destination = exit.transform.position;

            StartCoroutine(DestroyAfterReachedExit());
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
}
