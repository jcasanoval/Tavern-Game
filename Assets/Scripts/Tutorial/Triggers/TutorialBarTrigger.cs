using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBarTrigger : MonoBehaviour
{
    private TutorialManager tutorialManager;
    
    void Awake()
    {
        tutorialManager = FindObjectOfType<TutorialManager>();
    }
}
