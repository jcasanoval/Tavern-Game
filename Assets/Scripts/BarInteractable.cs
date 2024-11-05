using UnityEngine;
using TMPro;

public class BarInteractable : Interactable
{
    public int initialStock = 5;
    public TextMeshProUGUI beerDisplay;
    public Sprite hoverIcon;
    private TutorialManager tutorialManager;

    public int Stock
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

    void Start()
    {
        tutorialManager = FindObjectOfType<TutorialManager>();
        Stock = initialStock;
    }

    public override bool Interact()
    {
        return InteractFunctionality.Interact();
    }

    public override bool NPCInteract(GameObject npc)
    {
        return InteractFunctionality.NPCInteract(npc);
    }

    public override Sprite GetHoverIcon()
    {
        if (tutorialManager.IsInTutorialMode && tutorialManager.IsInStep(TutorialStep.ExplainMovement)) {
            return hoverIcon;
        }

        return null;
    }
}
