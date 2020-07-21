using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_spawn : StateMachineBehaviour
{

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<CapsuleCollider2D>().enabled = true;
        animator.GetComponent<Rigidbody2D>().gravityScale = 1f;
    }

}
