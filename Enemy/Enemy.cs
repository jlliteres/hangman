using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;
    public float patrolSpeed;
    public float followSpeed;
    public float attackRange = 1f;
    public int damage;

    public bool movingRight;
    public bool isBoss = false;
    public bool canBleed = false;
    public Transform groundCheck;
    public Transform attackPoint;
    public Transform attackPoint2;
    public LayerMask playerLayer;
    public GameObject fire;
    public GameObject ammo;

    private Transform player;
    private Animator animator;
    private Vector2 rayOffset;
    private Manager manager;

    ///Script enemy
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        animator = gameObject.GetComponent<Animator>();
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>();
    }

    ///Update de la posición del jugador para iniciar animación de ataque cuando el jugador está encima del enemigo
    private void FixedUpdate()
    {
        if (!isBoss) animator.SetFloat("IdleAttack", Mathf.Abs(player.position.x - transform.position.x));
    }

    ///Mirar al jugador
    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;
        
        if (transform.position.x > player.position.x && movingRight)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            movingRight = false;
        }
        else if (transform.position.x <= player.position.x && !movingRight)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            movingRight = true;
        }
    }

    ///Giro al llegar a un punto patrulla
    public void PatrolFlip()
    {
        movingRight = !movingRight;

        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        transform.localScale = flipped;
        transform.Rotate(0f, 180f, 0f);
    }

    ///Ataque normal
    public void Attack()
    {
        Debug.Log("Attacking");
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

        foreach (Collider2D player in hitPlayer)
        {
            Debug.Log("Player hit");
            if (fire != null)
            {
                GameObject fireSpawn = Instantiate(fire, attackPoint.position, Quaternion.identity, player.transform);
                Destroy(fireSpawn, 1f);
            }
            player.GetComponent<Player>().Hit(damage);
        }
    }

    ///Ataque jugador encima
    public void StandAttack()
    {
        //if (isBoss) return;

        Debug.Log("Attacking");

        Collider2D[] hitBack = Physics2D.OverlapCircleAll(attackPoint2.position, attackRange, playerLayer);

        foreach (Collider2D player in hitBack)
        {
            Debug.Log("Player hit");
            if(fire != null)
            {
                GameObject fireSpawn = Instantiate(fire, attackPoint.position, Quaternion.identity, player.transform);
                Destroy(fireSpawn, 1f);
            }
            player.GetComponent<Player>().Hit(damage);
        }
    }

    ///Disparar, boss lvl 1
    public void Shoot()
    {
       if (isBoss) Instantiate(ammo, attackPoint.position, attackPoint.rotation);
    }
    
    ///Recibir daño si no es invulnerable. Si es un boss genera poder
    public void Hit(float damage)
    {
        if (BossCucua.invulnerable) return;
        if (isBoss && manager.canPower) manager.IncreasePower(3);
        health -= damage;
        animator.SetTrigger("Hit");
        if (health <= 0)
        {
            if (isBoss) manager.Win();
            manager.IncreasePower(5);
            Destroy(this.gameObject);
        }
    }
}
