using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform exit;
    private ChairManager chairManager;

    private Chair lastSatChair;

    private AudioSource moneyTipAudioSource;
    private AudioSource angryAudioSource;
    private AudioSource[] audioSources;

    public SpriteRenderer spriteRenderer;
    public SpriteHolder spriteHolder;

    public Animator animator;

    private static float maxWaitTime = 20f;

    private bool isServed = false;
    public bool isSitting = false;

    private float timeWaited = 0f;

    public float PatienceLevel
    {
        get
        {
            if (isServed)
            {
                return 1;
            }
            return 1 - (timeWaited / maxWaitTime);
        }
    }

    void Awake()
    {
        audioSources = GetComponents<AudioSource>();
        moneyTipAudioSource = audioSources[0];
        angryAudioSource = audioSources[1];
    }

    void Start()
    {
        chairManager = FindObjectOfType<ChairManager>();
        agent = GetComponent<NavMeshAgent>();
        spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.sprite = spriteHolder.GetProfile();
        animator = gameObject.GetComponent<Animator>();

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
            lastSatChair = chairManager.GetChairByCustomer(this.gameObject);
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

        timeWaited = Random.Range(0, maxWaitTime / 2);
        animator.SetTrigger("Sit");


        while (timeWaited < maxWaitTime && !isServed)
        {
            timeWaited += Time.deltaTime;
            yield return null;
        }

        if (!isServed)
        {
            Debug.Log("Customer is leaving because they were not served.");
            angryAudioSource.Play();
        }
        else
        {
            Debug.Log("Customer is drinking the beer.");
            animator.SetTrigger("DrinkBeer");
            yield return new WaitForSeconds(10f);

            FindAnyObjectByType<GoldManager>().AddGold(2);
            moneyTipAudioSource.Play();
        }
        animator.SetTrigger("Stand");

        isSitting = false;

        chairManager.FreeChairForCustomer(this.gameObject);
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
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

    //Animation Controls

    public void AnimationSit()
    {
        spriteRenderer.sprite = spriteHolder.GetSitting(lastSatChair.Direction.y < 0);
    }

    public void AnimationDrink()
    {
        spriteRenderer.sprite = spriteHolder.GetDrinking(lastSatChair.Direction.y < 0);
    }

    public void AnimationLeave()
    {
        spriteRenderer.sprite = spriteHolder.GetProfile();
    }


}
