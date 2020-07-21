using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    public float speed, jump, offsetJump;
    public int health, maxHealth;
    public float reloadTime;

    public bool isFlipped = false;

    public Rigidbody2D rb;
    public GameObject ammo;
    public GameObject shootPoint;
    public Transform groundCheck;
    public AudioSource audioSource;

    public LayerMask groundMask;
    public Animator animator;
    public Camera mainCamera;

    private float h, v; //movement axis
    private bool isJumping, isGrounded, isShooting, canClimb;
    private Manager manager;


    ///Script jugador

    ///Condiciones iniciales
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        health = maxHealth;
        isShooting = false;
        canClimb = false;
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>();
        animator = gameObject.GetComponent<Animator>();
        transform.position = manager.savedPos;
    }

    ///Comprobar si se puede mover, girar cuando el ratón cambie de posición respecto al jugador.
    void Update()
    {
        if (!Manager.isPaused && Weapon.canMove)
        {
            if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x <= transform.position.x && !isFlipped)
            {
                Flip();
            }
            else if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x > transform.position.x && isFlipped)
            {
                Flip();
            }

            h = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            v = Input.GetAxis("Vertical") * speed * Time.deltaTime;

            transform.Translate(isFlipped ? -h : h, canClimb? v : 0, 0);
            animator.SetFloat("Speed", Mathf.Abs(h));

            ///Comprobar si el jugador puede saltar y saltar si puede
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                isJumping = true;
                Jump();
            }

            ///Empuje hacia abajo cuando el jugador suelta el botón de saltar. Sirve para controlar el salto del jugador. Más tiempo presionando, mayor salto.
            if (Input.GetButtonUp("Jump") && isJumping)
            {
                rb.AddForce(new Vector2(0, -jump * offsetJump), ForceMode2D.Impulse);
            }

            ///Si tiene munición o usa poder de munición infinita y no está disparando, disparar
            if (Input.GetMouseButtonDown(0) && (manager.ammo > 0 || manager.infiniteAmmo) && !isShooting)
            {
                StartCoroutine("Shoot");
            }
        }
    }

    ///Sonido de pasos
    public void StepSound()
    {
        audioSource.Play();
    }

    ///Comprobar si el jugador está tocando el suelo o saltando.
    private void FixedUpdate()
    {
        isGrounded = false;
        isJumping = true;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, 0.2f, groundMask);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                isGrounded = true;
                isJumping = false;
            }
        }
        animator.SetBool("IsJumping", isJumping);
    }

    ///Comprobar triggers.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Death"))///Muerte.
        {
            StartCoroutine("Death");
        }

        if(other.CompareTag("Ladder"))///Escaleras.
        {
            //transform.parent = other.transform;
            rb.gravityScale = 0;
            canClimb = true;
        }
    }

    ///Salida de triggers.
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))///Escaleras.
        {
            //transform.parent = null;
            rb.gravityScale = 2.5f;
            canClimb = false;
        }
    }

    ///Salto.
    private void Jump()
    {
        rb.AddForce(new Vector2(0, jump), ForceMode2D.Impulse);
    }

    ///Curar, recibe un porcentaje.
    public void Heal(int hp)
    {
        health += maxHealth * hp/100;
        if (health > maxHealth) health = maxHealth;
        manager.SetHealth(health);
    }

    ///Girar jugador.
    private void Flip()
    {
        isFlipped = !isFlipped;
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;
        mainCamera.transform.localScale = -flipped;
        mainCamera.transform.Rotate(0f, 180f, 0f);
        transform.localScale = flipped;
        transform.Rotate(0f, 180f, 0f);
    }

    ///Recibir daño.
    public void Hit(int damage)
    {
        health -= damage;
        animator.SetTrigger("Hit");
        manager.SetHealth(health);
        if (health <= 0) StartCoroutine("Death");
    }

    ///Corrutina de muerte.
    IEnumerator Death()
    {
        manager.canPause = false;
        Debug.Log("You died!");
        Weapon.canMove = false;
        animator.SetBool("IsDead", true);
        yield return new WaitForSeconds(1.5f);
        manager.Death();
    }

    ///Corrutina de disparo, espera tiempo de recarga.
    private IEnumerator Shoot()
    {

        shootPoint.GetComponent<SpriteRenderer>().enabled = false;
        isShooting = true;
        Instantiate(ammo, shootPoint.transform.position, shootPoint.transform.rotation);

        if(manager.tripleArrow)
        {
            Instantiate(ammo, shootPoint.transform.position, shootPoint.transform.rotation * Quaternion.Euler(0f, 0f, 10f));
            Instantiate(ammo, shootPoint.transform.position, shootPoint.transform.rotation * Quaternion.Euler(0f, 0f, -10f));
        }
        if (!manager.infiniteAmmo) manager.IncreaseAmmo(-1);
       

        yield return new WaitForSeconds(reloadTime);

        shootPoint.GetComponent<SpriteRenderer>().enabled = true;
        isShooting = false;
    }
}
