using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_kill : MonoBehaviour
{

    public LayerMask layerMask;

    ///Script plataformas lvl 3. Matan al jugador si está debajo.

    void Update()
    {
        RaycastHit2D hitPlayer = Physics2D.BoxCast(transform.position, new Vector2(2, .5f), 0, new Vector2(0, -1), 0f, layerMask);

        if(hitPlayer.collider != null && hitPlayer.collider.CompareTag("Player"))
        {
            hitPlayer.collider.GetComponent<Player>().StartCoroutine("Death");
        }
    }
}
