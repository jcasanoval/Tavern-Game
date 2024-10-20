using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExcaliburInteractable : Interactable
{
    private Animator animator;
    public DayCycleManager dayCycleManager;
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
        animator = GetComponentInParent<Animator>();
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
