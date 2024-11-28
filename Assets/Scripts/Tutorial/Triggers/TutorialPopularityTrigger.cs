using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPopularityTrigger : MonoBehaviour
{
    private TutorialManager tutorialManager;
    private AdviceManager adviceManager;
    private bool isPlayerInTrigger = false;
    [SerializeField] 
    private Sprite popularityAdvice;

    void Awake()
    {
        tutorialManager = FindObjectOfType<TutorialManager>();
        adviceManager = FindObjectOfType<AdviceManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player" && tutorialManager.IsInStep(TutorialStep.GoToPopularityPoster))
        {
            isPlayerInTrigger = true;
            adviceManager.ShowSprite(popularityAdvice);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player" && tutorialManager.IsInStep(TutorialStep.GoToPopularityPoster))
        {
            isPlayerInTrigger = false;
            tutorialManager.ProgressToNextStep();
            adviceManager.HideAdvice();
        }
    }

    public void UpdateAdvise()
    {
        if (isPlayerInTrigger && tutorialManager.IsInStep(TutorialStep.GoToPopularityPoster))
        {
            adviceManager.ShowSprite(popularityAdvice);
        }
        else
        {
            adviceManager.HideAdvice();
        }
    }
}
