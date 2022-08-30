using Easy2DPlayerMovement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunNotificationSquirrel : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject.Find("Squirrel").GetComponent<MovementManager>().animatorRun = true;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject.Find("Squirrel").GetComponent<MovementManager>().animatorRun = false;
    }
}
