using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tirolina : MonoBehaviour
{
    public string text;
    public Animator anim;
    public Transform player;

    private bool playerIn = false;
    private Manager manager;

    ///Script para la tirolina
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>();
        anim = this.gameObject.GetComponent<Animator>();
    }

    ///Comprobar si se pulsa la tecla necesaria y activar animación.
    private void Update()
    {
        if (playerIn && Input.GetKeyDown(KeyCode.E))
        {
            manager.DisplayInfo("");
            anim.SetTrigger("Activate");
            player.parent = this.transform;
            Weapon.canMove = false;
            player.GetComponent<Rigidbody2D>().gravityScale = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.transform;
            manager.DisplayInfo(text);
            playerIn = true;
        }
    }

    ///Al salir el jugador, el texto de información desaparece
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIn = false;
            manager.DisplayInfo("");
            player.parent = null;
            Weapon.canMove = true;
            other.GetComponent<Rigidbody2D>().gravityScale = 2.5f;
        }
    }
}
