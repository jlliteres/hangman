using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    ///Script fuego, dobla el daño de las flechas.

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Bolt"))
        {
            other.GetComponent<Ammo>().Double();
        }
    }
}
