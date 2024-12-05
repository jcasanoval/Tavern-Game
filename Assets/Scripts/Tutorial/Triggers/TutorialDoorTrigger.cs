using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDoorTrigger : MonoBehaviour
{
    private TutorialManager tutorialManager;
    
    void Awake()
    {
        tutorialManager = FindObjectOfType<TutorialManager>();
    }
}
