using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public enum TutorialStep
{
    GoWithUncle,
    Introduction,
    ExplainDoorInteraction,
    ExplainTemper,
    CustomerIsComing,
    ExplainMovement,
    TakeBeerToCustomer,
    WaitForCustomerToLeave,
    Farewell,
    UncleWalksAway,
    Completed
}

public class TutorialManager : MonoBehaviour
{
    public TutorialStep currentStep;
    private PlayerMovement playerMovement;
    public bool IsInTutorialMode { get; private set; }
    public GameObject uncle;
    public GameObject chatBubble;
    public NavMeshAgent playerNavMesh;
    public Vector3 uncleTalkDestination;
    private MessageManager messageManager;
    private AudioSource doorOpenAudioSource;
    private AudioSource doorCloseAudioSource;
    public AudioSource moneyTipAudioSource;
    public Transform door;
    private CustomerSpawner customerSpawner;
    private Customer friend;
    private ChairManager chairManager;
    private float height;

    void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        height = playerMovement.transform.position.y;
        uncleTalkDestination = uncle.transform.position + new Vector3(1.5f, 0, -0.4f);
        messageManager = FindObjectOfType<MessageManager>();
        AudioSource[] audioSources = GetComponents<AudioSource>();
        doorOpenAudioSource = audioSources[0];
        doorCloseAudioSource = audioSources[1];
        moneyTipAudioSource = audioSources[2];
        customerSpawner = FindObjectOfType<CustomerSpawner>();
        chairManager = FindObjectOfType<ChairManager>();
    }

    void Start()
    {
        uncle.SetActive(true);
        chatBubble.SetActive(false);
        playerMovement.inputEnabled = false;
        playerNavMesh.enabled = true;

        Interactable[] interactable = FindObjectsOfType<Interactable>();
        for (int i = 0; i < interactable.Length; i++)
        {
            interactable[i].InteractFunctionality = interactable[i].TutorialInteractFunctionality;
        }

        StartCoroutine(WaitAndStartIntroduction());

        IEnumerator WaitAndStartIntroduction()
        {
            yield return new WaitForSeconds(1);
            IsInTutorialMode = true;
            currentStep = TutorialStep.GoWithUncle;
            StartStep(currentStep);
        }
    }

    void Update()
    {
        if (IsInTutorialMode && Input.GetKeyDown(KeyCode.Escape))
        {
            uncle.SetActive(false);

            door.position = new Vector3(6.02080011f,0.753099978f,-4.6262002f);
            door.rotation = Quaternion.Euler(0, 0, 0);

            Customer customer = FindObjectOfType<Customer>();
            if (customer != null)
            {
                Destroy(customer.gameObject);
            }

            FinishTutorial();
        }
    }

    void FixedUpdate()
    {
        if (IsInTutorialMode && !playerNavMesh.enabled)
        {
            Vector3 position = playerMovement.transform.position;
            playerMovement.transform.position = new Vector3(position.x, height, position.z);
        }
    }


    public void ProgressToNextStep()
    {
        currentStep++;
        StartStep(currentStep);
    }

    public bool IsInStep(TutorialStep step)
    {
        return currentStep == step;
    }

    public void StartStep(TutorialStep step)
    {
        Debug.Log("Starting tutorial step: " + step);

        messageManager.ShowMessage(step);

        switch (step)
        {
            case TutorialStep.GoWithUncle:
                GoWithUncle();
                break;
            case TutorialStep.Introduction:
            case TutorialStep.ExplainDoorInteraction:
            case TutorialStep.ExplainTemper:
            case TutorialStep.Farewell:
                WaitInteraction();
                break;
            case TutorialStep.CustomerIsComing:
                OpenDoorAndSpawnCustomer();
                break;
            case TutorialStep.ExplainMovement:
                EnableMovement();
                break;
            case TutorialStep.WaitForCustomerToLeave:
                MakeCustomerDrinkAndLeave();
                break;
            case TutorialStep.UncleWalksAway:
                UncleWalksAway();
                break;
            case TutorialStep.Completed:
                FinishTutorial();
                break;
            default:
                Debug.Log("Tutorial step not handled: " + step);
                break;
        }
    }

    #region GoWithUncle

    private void GoWithUncle()
    {
        playerNavMesh.SetDestination(uncleTalkDestination);

        StartCoroutine(WaitForReachingUncleToStop());
    }

    IEnumerator WaitForReachingUncleToStop()
    {
        while (playerNavMesh.pathPending || playerNavMesh.remainingDistance > 0.5f)
        {
            yield return null;
        }

        chatBubble.SetActive(true);

        ProgressToNextStep();
    }

    #endregion

    #region Introduction, ExplainDoorInteraction, ExplainTemper, Farewell

    private void WaitInteraction() 
    {
        StartCoroutine(WaitInteractionToContinue());
    }

    IEnumerator WaitInteractionToContinue()
    {
        ChatBubble bubble = chatBubble.GetComponent<ChatBubble>();
        bubble.SetBubbleKey(true);

        while (!Input.GetKeyDown(KeyCode.E))
        {
            yield return null;
        }

        while (Input.GetKeyDown(KeyCode.E))
        {
            yield return null;
        }

        bubble.SetBubbleKey(false);

        ProgressToNextStep();
    }

    #endregion

    #region CustomerIsComing

    private void OpenDoorAndSpawnCustomer()
    {
        doorOpenAudioSource.Play();
        door.position = new Vector3(6.79f,0.753099978f,-5.5f);
        door.rotation = Quaternion.Euler(0, 90, 0);

        StartCoroutine(SpawnCustomer());
    }

    IEnumerator SpawnCustomer()
    {
        friend = customerSpawner.SpawnHuman();
        Vector3? chairPosition = chairManager.GetAvailableChairPosition(friend.gameObject);

        NavMeshAgent agent = friend.GetComponent<NavMeshAgent>();

        agent.destination = chairPosition.Value;
        friend.lastSatChair = chairManager.GetChairByCustomer(friend.gameObject);
        
        while (agent.pathPending || agent.remainingDistance > 0.5f)
        {
            yield return null;
        }

        friend.GetComponent<Animator>().SetTrigger("Sit");

        door.position = new Vector3(6.02080011f,0.753099978f,-4.6262002f);
        door.rotation = Quaternion.Euler(0, 0, 0);
        doorCloseAudioSource.Play();

        ProgressToNextStep();
    }

    #endregion

    #region ExplainMovement, Completed

    private void EnableMovement()
    {
        playerNavMesh.enabled = false;
        playerMovement.inputEnabled = true;
    }

    #endregion

    #region WaitForCustomerToLeave

    private void MakeCustomerDrinkAndLeave()
    {
        friend.GetComponent<Animator>().SetTrigger("DrinkBeer");
        
        StartCoroutine(WaitForCustomerToLeave());
    }

    IEnumerator WaitForCustomerToLeave()
    {
        yield return new WaitForSeconds(6f);

        moneyTipAudioSource.Play();

        playerMovement.inputEnabled = false;

        yield return new WaitForSeconds(0.3f);
        
        doorOpenAudioSource.Play();
        door.position = new Vector3(6.79f,0.753099978f,-5.5f);
        door.rotation = Quaternion.Euler(0, 90, 0);

        friend.GetComponent<Animator>().SetTrigger("Stand");

        chairManager.FreeChairForCustomer(friend.gameObject);
        friend.transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

        GameObject exit = GameObject.FindGameObjectWithTag("Finish");
        NavMeshAgent agent = friend.GetComponent<NavMeshAgent>();
        agent.destination = exit.transform.position;
        while (agent.pathPending || agent.remainingDistance > 0.5f)
        {
            yield return null;
        }

        Destroy(friend.gameObject);

        door.position = new Vector3(6.02080011f,0.753099978f,-4.6262002f);
        door.rotation = Quaternion.Euler(0, 0, 0);
        doorCloseAudioSource.Play();

        playerNavMesh.enabled = true;
        playerNavMesh.destination = playerNavMesh.transform.position;

        GoWithUncle();
    }

    #endregion

    #region UncleWalksAway

    private void UncleWalksAway()
    {
        StartCoroutine(WaitForUncleToLeave());
    }

    IEnumerator WaitForUncleToLeave()
    {
        chatBubble.SetActive(false);

        doorOpenAudioSource.Play();
        door.position = new Vector3(6.79f,0.753099978f,-5.5f);
        door.rotation = Quaternion.Euler(0, 90, 0);

        GameObject exit = GameObject.FindGameObjectWithTag("Finish");
        NavMeshAgent agent = uncle.GetComponent<NavMeshAgent>();
        agent.destination = exit.transform.position;
        while (agent.pathPending || agent.remainingDistance > 0.5f)
        {
            yield return null;
        }

        uncle.SetActive(false);

        door.position = new Vector3(6.02080011f,0.753099978f,-4.6262002f);
        door.rotation = Quaternion.Euler(0, 0, 0);
        doorCloseAudioSource.Play();

        ProgressToNextStep();
    }

    #endregion

    #region Completed

    private void FinishTutorial()
    {
        StopAllCoroutines();
        EnableMovement();
        Interactable[] interactable = FindObjectsOfType<Interactable>();
        for (int i = 0; i < interactable.Length; i++)
        {
            interactable[i].InteractFunctionality = interactable[i].DefaultInteractFunctionality;
        }

        StartCoroutine(WaitAndDisableTutorialMode());
    }

    IEnumerator WaitAndDisableTutorialMode()
    {
        yield return new WaitForSeconds(1);
        IsInTutorialMode = false;
    }

    #endregion
}
