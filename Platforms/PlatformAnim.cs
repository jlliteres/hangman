using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformAnim : MonoBehaviour
{

    public Animator anim;

    public float timer = .5f;
    private bool isFalling = false;

    ///Script para las plataformas con temporizador.
    
    void Start()
    {
        anim = this.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    ///Activar temporizador al entrar.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) StartCoroutine("Fall");
    }

    ///Reset temporizador al salir.
    private void OnCollisionExit2D(Collision2D collision)
    {
        StopCoroutine("Fall");
        if (!isFalling) anim.SetTrigger("Timer");
        isFalling = false;
    }

    ///Corrutina para caída de plataforma
    private IEnumerator Fall()
    {
        anim.SetTrigger("Timer");
        yield return new WaitForSeconds(timer);
        isFalling = true;
        anim.ResetTrigger("Timer");

    }
}
