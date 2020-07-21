using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Verdugo_projectile : MonoBehaviour
{
    public float speed;
    public int damage = 2;

    ///Script para proyectil boss lvl 1

    void Start()
    {
        Destroy(this.gameObject, 3f);
    }

    ///Movimiento
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime, Space.Self);
    }

    ///Impacto jugador, hacer daño y destruir
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();

            player.Hit(damage);

            Destroy(this.gameObject);
        }
    }
}
