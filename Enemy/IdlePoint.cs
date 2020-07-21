using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdlePoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Enemy"))
        {
            if (other.GetComponent<Animator>().GetBool("IsFollowing"))
            {
                other.GetComponent<Animator>().SetTrigger("Idle");
            }
        }
    }
}
