using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Idle : StateMachineBehaviour
{

    Enemy enemy;
    Transform player;
    Rigidbody2D rb;

    ///Script de comportamiento durante la animación de espera mientras ven al jugador. Si se aleja, pasan a patrullar.


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
        RaycastHit2D hit = Physics2D.Raycast(enemy.groundCheck.position, new Vector2(0, -1), .5f);

        enemy.LookAtPlayer();

        if (hit.collider != null)
        {
            animator.SetTrigger("Idle");
        }

        if (Mathf.Abs(Vector2.Distance(player.position, rb.position)) > 15f)
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
