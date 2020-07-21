using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public string lockedDoor;
    public string unlockableDoor;
    Animator anim;
    Manager manager;
    bool playerIn = false;

    ///Script para abri puertas. Comprueba que el jugador tenga la llave.

    private void Start()
    {
        anim = GetComponentInParent<Animator>();
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>();
    }

    private void Update()
    {
        if (playerIn && Input.GetKeyDown(KeyCode.E))
        {
            manager.DisplayInfo("");
            manager.hasKey = false;
            manager.Key(false);
            anim.SetTrigger("Activate");
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (manager.hasKey)
            {
                manager.DisplayInfo(unlockableDoor);
                playerIn = true;
            }
            else
            {
                manager.DisplayInfo(lockedDoor);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIn = false;
            manager.DisplayInfo("");           
        }
    }
}
