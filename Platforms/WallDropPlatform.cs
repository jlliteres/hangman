using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDropPlatform : MonoBehaviour
{
    Animator animator;

    ///Script para animación de plataformas con cadena. Se activa al impactar una flecha.
    
    private void Start()
    {
        animator = GetComponentInParent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Bolt"))
        {
            animator.SetTrigger("Drop");
            Destroy(this.gameObject);
        }
    }

}
