using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidePlatform : MonoBehaviour
{
    public int speed;

    ///Script para plataforma con movimiento lateral.

    private void FixedUpdate()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    ///"Atrapar" jugador.
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.collider.CompareTag("Player"))
        {
            other.transform.parent = this.transform;
        }
    }

    ///Liberar jugador.
    private void OnCollisionExit2D(Collision2D other)
    {
        if(other.collider.CompareTag("Player"))
        {
            other.transform.parent = null;
        }
    }

    ///Girar.
    public void Flip()
    {
        speed *= -1;
    }
}
