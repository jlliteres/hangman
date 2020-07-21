using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPoint : MonoBehaviour
{
    //Punto de patrulla para enemigos.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().PatrolFlip();
        }
        if (other.gameObject.CompareTag("Side Platform"))
        {
            other.GetComponent<SidePlatform>().Flip();
        }
    }
}
