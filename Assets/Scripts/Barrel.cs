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

    public override bool Interact()
    {
        if (dayCycleManager.IsOpen())
        {
            if (handController.HasFreeHands() && Stock > 0)
            {
                Stock--;
                handController.HoldMug();
                serveBeerAudioSource.Play();
                return true;
            }
        }
        else if (goldManager.SpendGold(1))
        {
            Stock++;
            return true;
        }
        return false;
    }
}
