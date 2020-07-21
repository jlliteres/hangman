using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cucua_idle : StateMachineBehaviour
{
    public float attackRange = 3f;

    Enemy enemy;
    Transform player;
    Rigidbody2D rb;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemy = animator.GetComponent<Enemy>();
        rb = animator.GetComponent<Rigidbody2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        enemy.LookAtPlayer();

        if (enemy.movingRight && player.position.x < rb.position.x)
        {
            animator.SetTrigger("Idle");
        }
        else if (!enemy.movingRight && player.position.x > rb.position.x)
        {
            animator.SetTrigger("Idle");
        }

        if (Mathf.Abs(Vector2.Distance(player.position, rb.position)) <= attackRange)
        {
            animator.SetTrigger("Attack");
        }

        if (Mathf.Abs(Vector2.Distance(player.position, rb.position)) > 5f)
        {
            animator.SetBool("IsFollowing", false);
        }
    }
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Idle");
    }
}
