using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potenciador : MonoBehaviour
{
    public int id;
    public Sprite sprite;
    public string playerprefs;

    private Manager manager;

    ///Script recogida de potenciador.

    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerPrefs.SetInt(playerprefs, 1);
            manager.Checkpoint(this.transform.position);
            manager.UnlockPot(id, sprite);
            Destroy(this.gameObject);
        }
    }
}
