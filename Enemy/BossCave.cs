using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCave : MonoBehaviour
{
    public GameObject mainCamera;
    public BoxCollider2D entry;
    public BoxCollider2D exit;
    public Transform playerPos;
    public BossRat bossRat;
    public AudioClip music;

    private bool playerEnter = false;

    private Animator animator;
    private Rigidbody2D player;

    ///Script espacio cueva boss lvl 2

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }
    private void FixedUpdate()
    {
        if (playerEnter)
        {
            Vector2 newPos = Vector2.MoveTowards(player.position, playerPos.position, 2.5f * Time.fixedDeltaTime);
            player.MovePosition(newPos);
        }
    }

    ///Comprobar si el jugador ha llegado e iniciar animación intro.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<SoundManager>().StopSound("LevelMusic");
            mainCamera.SetActive(false);
            player = other.GetComponent<Rigidbody2D>();
            playerEnter = true;
            other.GetComponent<Player>().enabled = false;
            other.GetComponent<AudioSource>().enabled = false;
            animator.SetTrigger("Intro");
        }
    }

    ///Iniciar pelea
    public void BossFight()
    {
        player.GetComponent<AudioSource>().enabled = true;
        FindObjectOfType<SoundManager>().PlaySound("BossMusic");
        playerEnter = false;
        mainCamera.SetActive(true);
        entry.isTrigger = false;
        exit.isTrigger = false;
        entry.GetComponent<SpriteRenderer>().enabled = true;
        exit.GetComponent<SpriteRenderer>().enabled = true;
        bossRat.enabled = true;
    }
}
