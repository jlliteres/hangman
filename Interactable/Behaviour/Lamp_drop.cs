using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp_drop : StateMachineBehaviour
{
    public Transform player;
    public Camera mainCamera;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Weapon.canMove = false;
        mainCamera = animator.GetComponent<Lamp>().mainCamera;
        mainCamera.transform.parent = animator.GetComponent<Lamp>().glass;
        mainCamera.transform.position = animator.GetComponent<Lamp>().glass.transform.position + new Vector3(0, 0, -10);
        mainCamera.orthographicSize = 3;
    }


    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Weapon.canMove = true;
        mainCamera.transform.parent = player;
        mainCamera.transform.position = player.position + new Vector3(0, 2, -10);
        mainCamera.orthographicSize = 7;
        Destroy(animator.GetComponent<Lamp>().lamp.gameObject);
    }
}
