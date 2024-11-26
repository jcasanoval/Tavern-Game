using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPopularityTrigger : MonoBehaviour
{
    private TutorialManager tutorialManager;

    void Awake()
    {
        tutorialManager = FindObjectOfType<TutorialManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ENTER trigger" + other.name);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player" && tutorialManager.IsInStep(TutorialStep.GoToPopularityPoster))
        {
            tutorialManager.ProgressToNextStep();
        }
    }
}
