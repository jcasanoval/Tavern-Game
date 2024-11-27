using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBarTrigger : MonoBehaviour
{
    private TutorialManager tutorialManager;
    private AdviceManager adviceManager;
    [SerializeField] 
    private Sprite nobodyToServeAdvice;
    
    void Awake()
    {
        tutorialManager = FindObjectOfType<TutorialManager>();
        adviceManager = FindObjectOfType<AdviceManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player" && tutorialManager.IsInStep(TutorialStep.BuyABeer))
        {
            adviceManager.ShowSprite(nobodyToServeAdvice);
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
