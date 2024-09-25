using UnityEngine;
using TMPro;

public class Barrel : Interactable
{
    private HandController handController;
    private DayCycleManager dayCycleManager;
    private GoldManager goldManager;
    private AudioSource serveBeerAudioSource;
    public int initialStock = 5;
    public TextMeshProUGUI beerDisplay;

    private int Stock
    {
        get
        {
            return stock;
        }
        set
        {
            stock = value;
            beerDisplay.text = stock.ToString();
        }
    }

    private int stock;

    void Awake()
    {
        serveBeerAudioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        Stock = initialStock;
        handController = FindObjectOfType<HandController>();
        dayCycleManager = FindObjectOfType<DayCycleManager>();
        goldManager = FindObjectOfType<GoldManager>();
    }

    public override void Interact()
    {
        if (dayCycleManager.IsOpen())
        {
            if (handController.HasFreeHands() && Stock > 0)
            {
                Stock--;
                handController.HoldMug();
                serveBeerAudioSource.Play();
            }
        }
        else if (goldManager.SpendGold(1))
        {
            Stock++;
        }
    }
}
