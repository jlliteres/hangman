using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRat : MonoBehaviour
{

    public Transform[] rute1;
    public Transform[] rute2;
    public Transform climbPoint;

    public Animator animator;
    public Rigidbody2D rb;
    public LayerMask layerMask;
    public CapsuleCollider2D collider1;
    public CapsuleCollider2D collider2;

    private bool followPlayer = false;
    private bool falling = false;
    private bool firstRute = true;
    private bool movingRight = false;
    private float speed;
    private int count = 0;

    private Enemy enemy;
    private Transform currentPoint;
    private Transform player;
  
    ///Script boss level 2
    
    void Start()
    {
        currentPoint = rute1[0];
        enemy = GetComponent<Enemy>();
        speed = enemy.followSpeed;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    ///La rata camina por el suelo y la pared hacia el punto de ruta. Al llegar al techo, entra en "followPlayer" y persigue al jugador hasta llegar a cierta distancia, se deja caer para provocar daños.
    ///Al llegar al suelo, se dirige a la ruta más cercana para iniciar el proceso.
    void FixedUpdate()
    {        

        if (!followPlayer && !falling)
        {

            RaycastHit2D hit = Physics2D.Raycast(climbPoint.position, new Vector2(-1, 0), 8f, layerMask);
            RaycastHit2D hit2 = Physics2D.Raycast(transform.position, new Vector2(-1, 0), 8f, layerMask);

            if(hit.collider != null && hit.collider.CompareTag("Player"))
            {
                collider1.isTrigger = true;
                collider2.isTrigger = true;
            }
            else
            {
                collider1.isTrigger = false;
                collider2.isTrigger = false;
            }

            if (hit2.collider != null && hit2.collider.CompareTag("Player"))
            {
                collider1.isTrigger = true;
                collider2.isTrigger = true;
            }
            else
            {
                collider1.isTrigger = false;
                collider2.isTrigger = false;
            }

            Vector2 target = new Vector2(currentPoint.position.x, currentPoint.position.y);
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

            if (Mathf.Abs(Vector2.Distance(climbPoint.position, target)) < .5f)
            {
                NextPoint();
            }
        }
        else
        {
            Vector2 target = new Vector2(player.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
            //transform.right = player.position - transform.position;

            Vector3 flipped = transform.localScale;
            flipped.z *= -1f;

            if (transform.position.x > player.position.x && !movingRight)
            {
                transform.localScale = flipped;
                transform.Rotate(0f, 180f, 0f);
                movingRight = true;
            }
            else if (transform.position.x <= player.position.x && movingRight)
            {
                transform.localScale = flipped;
                transform.Rotate(0f, 180f, 0f);
                movingRight = false;
            }

            //Debug.Log(Mathf.Abs(enemy.attackPoint.position.x - player.position.x));
            if (Mathf.Abs(enemy.attackPoint.position.x - player.position.x) <= 3f)
            {
                speed /= 1.5f;
                falling = true;
                followPlayer = false;
            }
        }

        if (falling)
        {
            rb.gravityScale = 2f;
            transform.Translate(Vector2.down * speed / 2 * Time.deltaTime, Space.World);

            RaycastHit2D hitGround = Physics2D.BoxCast(enemy.attackPoint.position, new Vector2(5, 1), 0, new Vector2(0, -1), 1, layerMask);
            if (hitGround.collider != null)
            {
                GetComponent<Enemy>().Attack();
                
                          

                //transform.position = hitGround.point;
                followPlayer = false;
                falling = false;

                if (transform.position.x > player.position.x)//Mathf.Abs(transform.position.x - rute1[0].position.x) <= Mathf.Abs(transform.position.x - rute2[0].position.x))
                {
                    firstRute = false;
                }
                else firstRute = true;

                Vector3 flipped = transform.localScale;
                flipped.x *= -1f;
                transform.localScale = flipped;

                if (movingRight)
                {
                    if (firstRute) transform.Rotate(180f, 180f, 180f);
                    else transform.Rotate(0, 180f, 0);
                }
                else
                {
                    if (firstRute) transform.Rotate(0, 180f, 0);
                    //else;
                }

                movingRight = !firstRute;

                NextPoint();
            }
        }

    }

    ///Nuevo punto de la ruta. Si es mayor al lenght del array, se reinicia el contador. Se ajusta la rotación del boss según el punto de la ruta en el que se encuentre.
    private void NextPoint()
    {
        count++;
        if (count >= rute1.Length || count >= rute2.Length)
        {
            followPlayer = true;
            count = -1;
        }
        else
        {
            Vector3 flipped = transform.localScale;
            flipped.y *= -1f;
            Vector3 rotation;
            switch(count)
            {
                case 0:
                    speed = enemy.followSpeed;
                    rotation = new Vector3(0, 180, 0);
                    break;
                case 1:
                    transform.position = currentPoint.position;
                    rb.gravityScale = 0;
                    speed /= 1.5f;
                    rotation = new Vector3(0, 180, 90);
                    break;
                case 2:
                    transform.position = currentPoint.position;
                    rotation = new Vector3(0, 180, -90);
                    break;
                default:
                    rotation = new Vector3();
                    break;

            }
            transform.localScale = flipped;
            transform.Rotate(rotation);
            currentPoint = firstRute ? rute1[count] : rute2[count];
            Debug.Log(currentPoint);
        }
    }
}
