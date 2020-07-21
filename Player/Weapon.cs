using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float offset;
    public static bool canMove = true;
    private bool isFlipped = false;

    ///Script arma del jugador

    ///El arma rota para apuntar en todo momento al ratón y se invierte verticalmente cuando cambia de lado.
    void Update()
    {
        if (Camera.main != null && !Manager.isPaused && canMove)
        {
            if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x <= transform.position.x && !isFlipped)
            {
                Flip();
            }
            else if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x > transform.position.x && isFlipped)
            {
                Flip();
            }

            Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float rotZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
        }

        if(!canMove)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 70f);
        }
    }

    private void Flip()
    {
        offset *= -1;
        isFlipped = !isFlipped;
        Vector3 flipped = transform.localScale;
        flipped.y *= -1f;

        transform.localScale = flipped;
        transform.Rotate(180f, 0f, 0f);
    }
}
