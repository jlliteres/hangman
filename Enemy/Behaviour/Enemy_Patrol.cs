using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Patrol : StateMachineBehaviour
{

    private float speed;
    public LayerMask layerMask;

    private Vector2 rayOffset;    

    Enemy enemy;
    Rigidbody2D rb;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<Enemy>();
        rb = animator.GetComponent<Rigidbody2D>();
        speed = enemy.patrolSpeed;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {    

        rayOffset = new Vector2(animator.transform.position.x + ((enemy.movingRight ? 1 : -1) * 0.55f), rb.position.y);
        RaycastHit2D hit = Physics2D.Raycast(rayOffset, new Vector2(1, 0), 8f, layerMask);
        RaycastHit2D hit2 = Physics2D.Raycast(rayOffset, new Vector2(-1, 0), 8f, layerMask);

        Debug.DrawRay(rayOffset, Vector2.right, Color.red);

        animator.transform.Translate(Vector2.right * speed * Time.deltaTime);

        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            animator.SetBool("IsFollowing", true);
        }
        else if (hit2.collider != null && hit2.collider.CompareTag("Player"))
        {
            animator.SetBool("IsFollowing", true);
        }
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }
    
}
