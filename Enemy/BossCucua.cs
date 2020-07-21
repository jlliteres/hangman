using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCucua : MonoBehaviour
{
    public Transform attackPoint;
    public GameObject player;
    public GameObject fire;
    public Sprite transformedSprite;
    public Enemy enemy;
    public bool isTransformed = false;

    public static bool invulnerable = false;

    private Animator animator;
    

    ///Script boss lvl 4
    
    void Start()
    {
        enemy = GetComponent<Enemy>();
        player = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating("FlameAttack", 1f, 2f);
        animator = GetComponent<Animator>();
    }

    ///Update de la vida para activar animación de transformación
    private void Update()
    {
       animator.SetFloat("Health",enemy.health);    
    }
    
    ///Mirar siempre al jugador
    void FixedUpdate()
    {
        enemy.LookAtPlayer();
    }

    ///Instanciar bola de fuego
    public void FlameAttack()
    {
        Instantiate(fire, attackPoint.position, Quaternion.identity);
        /*if (isTransformed)
        {
            Instantiate(fire, attackPoint.transform.position, Quaternion.identity * Quaternion.Euler(0f, 0f, 10f));
            Instantiate(fire, attackPoint.transform.position, Quaternion.identity * Quaternion.Euler(0f, 0f, -10f));
        }*/
    }

    ///Ataque normal con daño normal
    public void NormalAttack()
    {
        Collider2D[] hit = Physics2D.OverlapCircleAll(attackPoint.position, enemy.attackRange, enemy.playerLayer);

        foreach (Collider2D player in hit)
        {
            Debug.Log("Player hit");            
            player.GetComponent<Player>().Hit(enemy.damage);
        }
    }

    ///Ataque con daño mortal
    public void InstaKill()
    {
        Collider2D[] hit = Physics2D.OverlapCircleAll(attackPoint.position, enemy.attackRange, enemy.playerLayer);

        foreach (Collider2D player in hit)
        {
            Debug.Log("Player hit");
            player.GetComponent<Player>().StartCoroutine("Death");
        }
    }
}
