using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDoorTrigger : MonoBehaviour
{
    private TutorialManager tutorialManager;
    private AdviceManager adviceManager;
    [SerializeField] 
    private Sprite needBeerAdvice;
    
    void Awake()
    {
        tutorialManager = FindObjectOfType<TutorialManager>();
        adviceManager = FindObjectOfType<AdviceManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player" && tutorialManager.IsInStep(TutorialStep.BuyABeer))
        {
            adviceManager.ShowSprite(needBeerAdvice);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player" && tutorialManager.IsInStep(TutorialStep.BuyABeer))
        {
            adviceManager.HideAdvice();
        }
    }
}
