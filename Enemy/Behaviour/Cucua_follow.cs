using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cucua_follow : StateMachineBehaviour
{
    private float speed;
    public float attackRange = 3f;
    public LayerMask layerMask;


    private Vector2 rayOffset;

    Enemy enemy;
    Transform player;
    Rigidbody2D rb;

    ///Script de comportamiento durante la animación de caminar para seguir al jugador y cuando llega a rango ataca. Si se aleja demasiado, pasa a patrullar.

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemy = animator.GetComponent<Enemy>();
        rb = animator.GetComponent<Rigidbody2D>();
        speed = enemy.followSpeed;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy.LookAtPlayer();
        
        Vector2 target = new Vector2(player.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);

        if (Mathf.Abs(Vector2.Distance(player.position, rb.position)) <= attackRange)
        {
            animator.SetTrigger("Attack");
        }

        if (Mathf.Abs(Vector2.Distance(player.position, rb.position)) > 15f)
        {
            animator.SetBool("IsFollowing", false);
        }        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
        animator.ResetTrigger("Idle");
    }

}
