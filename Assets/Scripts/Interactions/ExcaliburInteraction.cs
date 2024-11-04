using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExcaliburInteraction : IInteractFunctionality
{
    private Animator animator;
    private DayCycleManager dayCycleManager;
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
        animator = GetComponentInParent<ExcaliburInteractable>().GetComponentInParent<Animator>();
        dayCycleManager = FindObjectOfType<DayCycleManager>();
    }

    public override bool Interact(){
        if(dayCycleManager.IsOpen()){
            return false;
        }
        IsGrabbed = !IsGrabbed;
        return true;
    }

    public void ReturnExcalibur(){
        IsGrabbed = false;
    }
}
