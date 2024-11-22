using UnityEngine;

public class BarInteraction : IInteractFunctionality
{
    private HandController handController;
    private DayCycleManager dayCycleManager;
    private BarInteractable barInteractable;
    private GoldManager goldManager;
    private AudioSource serveBeerAudioSource;
    public Sprite hoverIcon;
    private PlayerInteraction playerInteraction;

    void Awake()
    {
        barInteractable = GetComponentInParent<BarInteractable>();
        serveBeerAudioSource = barInteractable.GetComponent<AudioSource>();
        handController = FindObjectOfType<HandController>();
        dayCycleManager = FindObjectOfType<DayCycleManager>();
        goldManager = FindObjectOfType<GoldManager>();
        playerInteraction = FindObjectOfType<PlayerInteraction>();
    }

    public override bool Interact()
    {
        if (dayCycleManager.IsOpen())
        {
            if (handController.HasFreeHands() && barInteractable.Stock > 0)
            {
                barInteractable.Stock--;
                handController.HoldMug();
                serveBeerAudioSource.Play();
                playerInteraction.HideHover(barInteractable);
                return true;
            }
        }
        
        return false;
    }

    public override bool NPCInteract(GameObject npc)
    {
        if(npc.tag != "Employee"){
            return false;
        }
        Employee employee = npc.GetComponent<Employee>();
        if (barInteractable.Stock > 0 && !employee.HasBeer)
        {
            barInteractable.Stock--;
            employee.HasBeer = true;
            return true;
        }
        return true;
    }

    public override Sprite GetHoverIcon()
    {
        if (dayCycleManager.IsOpen() && handController.HasFreeHands() && barInteractable.Stock > 0)
        {
            return hoverIcon;
        }

        return null;
    }
}
