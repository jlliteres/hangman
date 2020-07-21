using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public string text;
    public float timer = 1;
    public bool canActivate;

    public Animator anim;
    public GameObject[] platforms;

    private Manager manager;
    private bool playerIn = false;

    ///Script para la palanca que acciona animaciones
    void Start()
    {
        canActivate = true;
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>();
        anim = this.gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        if (playerIn && Input.GetKeyDown(KeyCode.E) && canActivate)
        {
            manager.DisplayInfo("");
            StartCoroutine("Activate");
        }
    }

    ///Cuando el jugador entra, se muestra por pantalla las instrucciones de activación (ej: pulsar una tecla)
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player")  && canActivate)
        {
            playerIn = true;
            manager.DisplayInfo(text);
        }
    }

    ///Al salir el jugador, el texto de información desaparece
    private void OnTriggerExit2D(Collider2D other)
    {
        manager.DisplayInfo("");
        playerIn = false;
    }

    ///Corrutina para activar las animaciones de manera escalonada
    private IEnumerator Activate()
    {
        canActivate = false;
        anim.SetTrigger("Activate");
        foreach (GameObject plat in platforms)
        {
            plat.GetComponent<Animator>().SetTrigger("Activate");
            yield return new WaitForSeconds(timer);                       
        }
    }

    ///Reset a posición inicial para poder activar de nuevo
    public void Reset()
    {
        canActivate = true;
        anim.SetTrigger("Activate");
    }
}
