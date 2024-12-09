using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExcaliburInteraction : IInteractFunctionality
{
    private Animator animator;
    private DayCycleManager dayCycleManager;
    public ExcaliburInteractable excaliburInteractable;
    public Sprite hoverIcon;
    private bool _isGrabbed = false;

    private bool _CoolDown = false;
    public bool IsGrabbed 
    {   
        get{
            return _isGrabbed;
        }
        set{
            animator.SetBool("IsGrabbed", value);
            if(value){
                OverSeerObserver.Instance.Notify(OverSeerEvent.Excalibur_Unsheathe);
            }else{
                OverSeerObserver.Instance.Notify(OverSeerEvent.Excalibur_Sheathe);
            }
            _isGrabbed = value;
        } 
    }

    void Awake()
    {
        animator = GetComponentInParent<ExcaliburInteractable>().GetComponentInParent<Animator>();
        dayCycleManager = FindObjectOfType<DayCycleManager>();
    }

    public override bool Interact(){
        if(dayCycleManager.IsOpen() || _CoolDown){
            return false;
        }
        
        IsGrabbed = !IsGrabbed;
        _CoolDown = true;
        Invoke("CoolDown", 3);
        return true;
    }

    private void CoolDown(){
        _CoolDown = false;
    }

    public void ReturnExcalibur(){
        IsGrabbed = false;
        OverSeerObserver.Instance.Notify(OverSeerEvent.Excalibur_Used);
    }
    
    public override Sprite GetHoverIcon()
    {
        if (!dayCycleManager.IsOpen())
        {
            return hoverIcon;
        }

        return null;
    }
}
