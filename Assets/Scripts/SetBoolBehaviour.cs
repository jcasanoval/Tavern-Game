using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBoolBehaviour : StateMachineBehaviour
{
    public string boolName;
    public bool onState;
    public bool onStateMachine;
    public bool valueOnEnter;
    public bool valueOnExit;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(onState)
            animator.SetBool(boolName, valueOnEnter);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(onState)
            animator.SetBool(boolName, valueOnExit);
    }
    
    override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        if(onStateMachine)
            animator.SetBool(boolName, valueOnEnter);
    }

    override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        if(onStateMachine)
            animator.SetBool(boolName, valueOnExit);
    }


}
