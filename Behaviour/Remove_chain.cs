using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Remove_chain : StateMachineBehaviour
{
    ///Script de comportamiento durante la animación de intro de boss lvl 4. Elimina la cadena de la plataforma que se cierra detrás del jugador.

     //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Destroy(animator.GetComponentInChildren<WallDropPlatform>().gameObject);
    }    
}
