using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR;

public enum TutorialStep
{
    Movement,
    BuyABeer,
    OpenTheBar,
    FirstNPCJoinsAndWaitForBeer,
    GetBeer,
    TakeBeerToCustomer,
    FirstNPCDrinkAndLeave,
    CloseTheBar,
    OpenTheBarAgain,
    SecondNPCJoinsAndWaitForBeer,
    GetSecondBeer,
    TakeBeerToSecondCustomer,
    SecondCustomerDrinkAndLeave,
    GoToPopularityPoster,
    CloseTheBarAgain,
    Completed
}

public class TutorialManager : MonoBehaviour
{
    public TutorialStep currentStep = TutorialStep.Completed;
    private PlayerMovement playerMovement;
    public bool IsInTutorialMode { get; private set; }
    public NavMeshAgent playerNavMesh;
    public AudioSource moneyTipAudioSource;
    public Transform door;
    private CustomerSpawner customerSpawner;
    private Customer npc;
    private ChairManager chairManager;
    private GoldManager goldManager;
    private BarInteractable barInteractable;
    private StockProviderInteractable stockProviderInteractable;
    private HandController handController;
    private float height;
    private DoorInteractable doorInteractable;
    [SerializeField] 
    private Sprite movementAdvice;
    [SerializeField]
    private Sprite buyBeerDuringDayAdvice;
    private AdviceManager adviceManager;
    private DirectionIndicator directionIndicator;
    public Transform posterAdmirationPosition;

    void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        height = playerMovement.transform.position.y;
        AudioSource[] audioSources = GetComponents<AudioSource>();
        moneyTipAudioSource = audioSources[2];
        customerSpawner = FindObjectOfType<CustomerSpawner>();
        chairManager = FindObjectOfType<ChairManager>();
        goldManager = FindObjectOfType<GoldManager>();
        barInteractable = FindObjectOfType<BarInteractable>();
        stockProviderInteractable = FindObjectOfType<StockProviderInteractable>();
        doorInteractable = FindObjectOfType<DoorInteractable>();
        handController = FindObjectOfType<HandController>();
        adviceManager = FindObjectOfType<AdviceManager>();
        directionIndicator = FindObjectOfType<DirectionIndicator>();
    }

    void Start()
    {
        Interactable[] interactable = FindObjectsOfType<Interactable>();
        for (int i = 0; i < interactable.Length; i++)
        {
            interactable[i].InteractFunctionality = interactable[i].TutorialInteractFunctionality;
        }

        StartCoroutine(WaitAndStartMovementStep());

        IEnumerator WaitAndStartMovementStep()
        {
            playerMovement.inputEnabled = false;
            yield return new WaitForSeconds(0.2f);
            IsInTutorialMode = true;
            currentStep = TutorialStep.Movement;
            StartStep(currentStep);
        }
    }

    void Update()
    {
        if (IsInTutorialMode && Input.GetKeyDown(KeyCode.Escape))
        {
            door.position = new Vector3(6.02080011f,0.753099978f,-4.6262002f);
            door.rotation = Quaternion.Euler(0, 0, 0);

            Customer[] customers = FindObjectsOfType<Customer>();
            foreach (Customer customer in customers)
            {
                chairManager.FreeChairForCustomer(customer.gameObject);
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
        currentStep = step;
        adviceManager.HideAdvice();
        directionIndicator.ClearTarget();

        switch (step)
        {
            case TutorialStep.Movement:
                StartTutorial();
                break;
            case TutorialStep.BuyABeer:
                BuyABeer();
                break;
            case TutorialStep.OpenTheBar:
                ShowOpenBarAdvice();
                break;
            case TutorialStep.FirstNPCJoinsAndWaitForBeer:
                SpawnFirstNPC();
                break;
            case TutorialStep.GetBeer:
                GetBeer();
                break;
            case TutorialStep.TakeBeerToCustomer:
                TakeBeerToCustomer();
                break;
            case TutorialStep.FirstNPCDrinkAndLeave:
                FirstNPCDrinkAndLeave();
                break;
            case TutorialStep.CloseTheBar:
                CloseTheBar();
                break;
            case TutorialStep.OpenTheBarAgain:
                break;
            case TutorialStep.SecondNPCJoinsAndWaitForBeer:
                SpawnSecondNPCAndWaitForBeer();
                break;
            case TutorialStep.GetSecondBeer:
                SkipStepIfNecessary();
                break;
            case TutorialStep.TakeBeerToSecondCustomer:
                break;
            case TutorialStep.SecondCustomerDrinkAndLeave:
                break;
            case TutorialStep.GoToPopularityPoster:
                GoToPopularityPoster();
                break;
            case TutorialStep.CloseTheBarAgain:
                CloseTheBarAgain();
                break;
            case TutorialStep.Completed:
                FinishTutorial();
                break;
            default:
                Debug.Log("Tutorial step not handled: " + step);
                break;
        }
    }

    #region Movement

    private void StartTutorial()
    {
        EnableMovement();
        adviceManager.ShowSprite(movementAdvice);
        StartCoroutine(DetectMovementKeys());

        IEnumerator DetectMovementKeys()
        {
            bool wPressed = false;
            bool aPressed = false;
            bool sPressed = false;
            bool dPressed = false;

            while (!wPressed || !aPressed || !sPressed || !dPressed)
            {
                if (!wPressed && Input.GetKeyDown(KeyCode.W)) wPressed = true;
                if (!aPressed && Input.GetKeyDown(KeyCode.A)) aPressed = true;
                if (!sPressed && Input.GetKeyDown(KeyCode.S)) sPressed = true;
                if (!dPressed && Input.GetKeyDown(KeyCode.D)) dPressed = true;

                yield return null;
            }

            adviceManager.HideAdvice();

            ProgressToNextStep();
        }
    }

    #endregion

    #region BuyABeer

    private void BuyABeer()
    {
        directionIndicator.SetTarget(stockProviderInteractable.transform);
    }

    #endregion

    #region OpenTheBar

    private void ShowOpenBarAdvice()
    {
        adviceManager.ShowSprite(buyBeerDuringDayAdvice);
        directionIndicator.SetTarget(door.transform);
    }

    #endregion

    #region FirstNPCJoinsAndWaitForBeer

    private void SpawnFirstNPC()
    {
        npc = customerSpawner.SpawnHuman();
        Vector3? chairPosition = chairManager.GetAvailableChairPosition(npc.gameObject);

        NavMeshAgent agent = npc.GetComponent<NavMeshAgent>();

        agent.destination = chairPosition.Value;
        npc.lastSatChair = chairManager.GetChairByCustomer(npc.gameObject);
        
        StartCoroutine(WaitForFirstNPCToSit(agent));
    }

    IEnumerator WaitForFirstNPCToSit(NavMeshAgent agent)
    {
        while (agent.pathPending || agent.remainingDistance > 0.5f)
        {
            yield return null;
        }

        npc.GetComponent<Animator>().SetTrigger("Sit");

        npc.isSitting = true;
        npc.isServed = false;

        ProgressToNextStep();
    }

    #endregion

    #region GetBeer

    private void GetBeer()
    {
        directionIndicator.SetTarget(barInteractable.transform);
    }

    #endregion

    #region TakeBeerToCustomer

    private void TakeBeerToCustomer()
    {
        directionIndicator.SetTarget(npc.transform);
    }

    #endregion

    #region FirstNPCDrinkAndLeave

    private void FirstNPCDrinkAndLeave()
    {
        npc.DrinkBeerAndLeave();
    }

    #endregion

    #region CloseTheBar

    private void CloseTheBar()
    {
        doorInteractable.CloseDoor();
        ProgressToNextStep();
    }

    #endregion

    #region SecondNPCJoinsAndWaitForBeer

    private void SpawnSecondNPCAndWaitForBeer()
    {
        npc = customerSpawner.SpawnHuman();
        Vector3? chairPosition = chairManager.GetAvailableChairPosition(npc.gameObject);

        NavMeshAgent agent = npc.GetComponent<NavMeshAgent>();

        agent.destination = chairPosition.Value;
        npc.lastSatChair = chairManager.GetChairByCustomer(npc.gameObject);
        
        StartCoroutine(WaitForSecondNPCToSit(agent));
    }

    IEnumerator WaitForSecondNPCToSit(NavMeshAgent agent)
    {
        while (agent.pathPending || agent.remainingDistance > 0.5f)
        {
            yield return null;
        }

        npc.GetComponent<Animator>().SetTrigger("Sit");

        npc.isSitting = true;
        npc.isServed = false;

        ProgressToNextStep();

        npc.WaitForSecondsAndLeave(20f);
    }

    #endregion

    #region GetSecondBeer

    private void SkipStepIfNecessary()
    {
        if (!FindObjectOfType<HandController>().HasFreeHands())
        {
            ProgressToNextStep();
        }
    }

    #endregion

    #region GoToPopularityPoster

    private void GoToPopularityPoster()
    {
        FindObjectOfType<TutorialPopularityTrigger>().UpdateAdvise();
        StartCoroutine(GoToPopularityPosterCoroutine());    
    }

    IEnumerator GoToPopularityPosterCoroutine()
    {
        playerMovement.inputEnabled = false;

        yield return new WaitForSeconds(0.5f);

        playerNavMesh.enabled = true;
        playerNavMesh.SetDestination(posterAdmirationPosition.position);

        Animator playerAnimator = playerMovement.GetComponent<Animator>();
        playerAnimator.SetBool("IsMoving", true);

        while (playerNavMesh.pathPending || playerNavMesh.remainingDistance > 0.5f)
        {
            yield return null;
        }

        playerAnimator.SetBool("IsMoving", false);

        EnableMovement();
    }

    private void EnableMovement()
    {
        playerNavMesh.enabled = false;
        playerMovement.inputEnabled = true;
    }

    private void DisableMovement()
    {
        playerMovement.inputEnabled = false;
        playerNavMesh.enabled = true;
    }

    #endregion

    #region CloseTheBarAgain

    private void CloseTheBarAgain()
    {
        doorInteractable.CloseDoor();
        ProgressToNextStep();
    }

    #endregion

    #region Completed

    private void FinishTutorial()
    {
        OverSeerObserver.Instance.Notify(OverSeerEvent.TutorialCompleted);

        adviceManager.HideAdvice();
        directionIndicator.ClearTarget();
        StopAllCoroutines();
        EnableMovement();
        handController.ReleaseMug();
        currentStep = TutorialStep.Completed;
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
