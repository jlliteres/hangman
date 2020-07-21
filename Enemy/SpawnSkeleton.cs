using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSkeleton : MonoBehaviour
{
    public LayerMask layerMask;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(1, 0), 8f, layerMask);
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position, new Vector2(-1, 0), 8f, layerMask);
        Debug.DrawRay(transform.position, Vector2.right, Color.red);

        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            FindObjectOfType<SoundManager>().PlaySound("SkeletonAssemble");
            GetComponent<Animator>().SetTrigger("Intro");
            Destroy(this);
        }
        else if (hit2.collider != null && hit2.collider.CompareTag("Player"))
        {
            FindObjectOfType<SoundManager>().PlaySound("SkeletonAssemble");
            GetComponent<Animator>().SetTrigger("Intro");
            Destroy(this);
        }
    }
}
