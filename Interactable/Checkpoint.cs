using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public AudioClip sound;

    Manager manager;

    private void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            FindObjectOfType<SoundManager>().PlaySound("Checkpoint");
            GetComponent<Animator>().SetTrigger("Activate");
            manager.Checkpoint(this.transform.position);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
