using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{

    public int damage;
    public float speed;
    public Transform player;

    ///Script para bola de fuego boss lvl 4
     
    ///Ajustar rotación para mirar al jugador
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        transform.rotation = Quaternion.Euler(0, 0, 90);
        transform.up = transform.position - player.position;
        Destroy(this.gameObject, 3f);
    }

    ///Movimiento
    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime, Space.Self);
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
