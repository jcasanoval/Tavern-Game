using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialExcaliburInteraction : IInteractFunctionality
{
    private Animator animator;
    private DayCycleManager dayCycleManager;
    private TutorialManager tutorialManager;
    public Sprite hoverIcon;
    private bool _isGrabbed = false;
    public bool IsGrabbed 
    {   
        get{
            return _isGrabbed;
        }
        set{
            animator.SetBool("IsGrabbed", value);
            _isGrabbed = value;
        } 
    }

    void Awake()
    {
        ExcaliburInteractable excaliburInteractable = GetComponentInParent<ExcaliburInteractable>();
        animator = excaliburInteractable.GetComponentInParent<Animator>();
        dayCycleManager = FindObjectOfType<DayCycleManager>();
        tutorialManager = FindObjectOfType<TutorialManager>();
    }

    public void ReturnExcalibur(){
        IsGrabbed = false;
    }

    public override bool Interact(){
        if (tutorialManager.IsInStep(TutorialStep.OpenTheBarAgain)){
            IsGrabbed = !IsGrabbed;
            return true;
        }

        return false;
    }
    
    public override Sprite GetHoverIcon()
    {
        if (tutorialManager.IsInStep(TutorialStep.OpenTheBarAgain))
        {
            return hoverIcon;
        }

        return null;
    }
}
