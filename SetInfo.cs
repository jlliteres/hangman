using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetInfo : MonoBehaviour
{
    public Manager manager;
    public string text;

    private void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            manager.DisplayInfo(text);
        }
    }

    //Al salir el jugador, el texto de información desaparece
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            manager.DisplayInfo("");
        }
    }
}
