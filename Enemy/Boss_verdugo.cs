using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_verdugo : MonoBehaviour
{
    public GameObject mainCamera;
    public BoxCollider2D entry;
    public Transform playerPos;
    public Enemy enemy;
    public AudioClip music;

    private bool playerEnter = false;

    private Animator animator;
    private Rigidbody2D player;

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

    public void BossFight()
    {
        player.GetComponent<AudioSource>().enabled = true;
        FindObjectOfType<SoundManager>().PlaySound("BossMusic");
        entry.gameObject.SetActive(false);
        playerEnter = false;
        mainCamera.SetActive(true);
        if (enemy != null) enemy.enabled = true;
    }
}
