using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform exit;
    private ChairManager chairManager;

    public Chair lastSatChair;

    private AudioSource moneyTipAudioSource;
    private AudioSource angryAudioSource;
    private AudioSource[] audioSources;

    private TutorialManager tutorialManager;
    private HandController handController;
    private BarInteractable barInteractable;

    public SpriteRenderer spriteRenderer;
    public SpriteHolder spriteHolder;

    public Animator animator;

    public ParticleSystem moneyParticle;

    private static float maxWaitTime = 20f;

    public bool isServed = false;
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
        chairManager = FindObjectOfType<ChairManager>();
        agent = GetComponent<NavMeshAgent>();
        spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        animator = gameObject.GetComponent<Animator>();
        tutorialManager = FindObjectOfType<TutorialManager>();
        handController = FindObjectOfType<HandController>();
        barInteractable = FindObjectOfType<BarInteractable>();
    }

    void Start()
    {
        GameObject exitObject = GameObject.FindGameObjectWithTag("Finish");
        if (exitObject != null)
        {
            exit = exitObject.transform;
        }
        else
        {
            Debug.LogError("Exit with tag 'Finish' not found.");
        }
    }

    public void GoToTheBar()
    {
        spriteRenderer.sprite = spriteHolder.GetProfile();

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
            OverSeerObserver.Instance.Notify(OverSeerEvent.Customer_Arrived);
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
        chairManager.SitOnChair(lastSatChair);
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
            FindAnyObjectByType<PopularityManager>().IncreasePopularity(-0.2f);
            angryAudioSource.Play();
            chairManager.AngrilyLeaveChair(this.gameObject);
            OverSeerObserver.Instance.Notify(OverSeerEvent.Customer_Leaves);
        }
        else
        {
            Debug.Log("Customer is drinking the beer.");
            animator.SetTrigger("DrinkBeer");
            yield return new WaitForSeconds(Random.Range(3f, 10f));

            FindAnyObjectByType<GoldManager>().AddGold(2);
            moneyParticle.Play();
            moneyTipAudioSource.Play();
            OverSeerObserver.Instance.Notify(OverSeerEvent.SoldBeer);
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
            FindAnyObjectByType<PopularityManager>().IncreasePopularity(PatienceLevel / 5);
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

        if (tutorialManager.IsInTutorialMode && isServed)
        {
            tutorialManager.ProgressToNextStep();
        }
        else if (tutorialManager.IsInTutorialMode && !isServed)
        {
            if (barInteractable.Stock > 0 || !handController.HasFreeHands())
            {
                tutorialManager.StartStep(TutorialStep.SecondNPCJoinsAndWaitForBeer);
            }
            else
            {
                tutorialManager.StartStep(TutorialStep.CloseTheBar);
            }
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

    public void DrinkBeerAndLeave()
    {
        StartCoroutine(DrinkBeerAndLeaveCoroutine());
    }

    IEnumerator DrinkBeerAndLeaveCoroutine()
    {
        isServed = true;
        animator.SetTrigger("DrinkBeer");
        yield return new WaitForSeconds(5f);
        FindAnyObjectByType<GoldManager>().AddGold(2);
        moneyTipAudioSource.Play();
        OverSeerObserver.Instance.Notify(OverSeerEvent.SoldBeer);
        animator.SetTrigger("Stand");
        isSitting = false;
        chairManager.FreeChairForCustomer(this.gameObject);
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        Exit();
    }

    public void WaitForSecondsAndLeave(float timeToWait)
    {
        StartCoroutine(WaitForSecondsAndLeaveCoroutine(timeToWait));
    }

    IEnumerator WaitForSecondsAndLeaveCoroutine(float timeToWait)
    {
        isSitting = true;
        timeWaited = 0;
        maxWaitTime = timeToWait;
        animator.SetTrigger("Sit");

        while (timeWaited < maxWaitTime && !isServed)
        {
            timeWaited += Time.deltaTime;
            yield return null;
        }

        if (!isServed)
        {
            angryAudioSource.Play();
        }
        else
        {
            animator.SetTrigger("DrinkBeer");
            yield return new WaitForSeconds(5f);

            FindAnyObjectByType<GoldManager>().AddGold(2);
            moneyTipAudioSource.Play();
            OverSeerObserver.Instance.Notify(OverSeerEvent.SoldBeer);
        }
        animator.SetTrigger("Stand");

        isSitting = false;

        chairManager.FreeChairForCustomer(this.gameObject);
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        Exit();
    }
}
