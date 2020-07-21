using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public float speed;
    public float damage = 2;
    public float damageMult;

    public AudioClip audioClip;
    public GameObject fire;
    public GameObject blood;
    public Transform point;
    public Manager manager;

    private bool doubled;

    ///Script flechas jugador

    void Start()
    {

        //Debug.Log(damage);
        doubled = false;
        Destroy(this.gameObject, 3f);
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>();
        FindObjectOfType<SoundManager>().PlaySound("Bolt");
        if (manager.extraDamage) damage *= damageMult;
    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime, Space.Self);
    }
    ///Dañar enemigo al colisionar. Si puede sangrar se instancia
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();

            enemy.Hit(damage);
            if(enemy.canBleed)
            {
                GameObject bloodClone = Instantiate(blood, point.transform.position, Quaternion.identity * (enemy.movingRight? Quaternion.Euler(0, 0, 0):Quaternion.Euler(0, 180, 0)), other.transform);
                Destroy(bloodClone.gameObject, 1f);
            }
            Destroy(this.gameObject);
        }
        if (other.CompareTag("TileMap Collider")) Destroy(this.gameObject);
    }

    ///Función para doblar daño al atravesar un fuego
    public void Double()
    {
        if (doubled) return;

        doubled = true;
        damage *= 2;
        Instantiate(fire, point.position, Quaternion.identity, this.transform);
    }
}
