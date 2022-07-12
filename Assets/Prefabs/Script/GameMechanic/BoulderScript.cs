using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderScript : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb;
    [SerializeField] bool Rotation;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        if (!Rotation)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bomb")
        {
            Destroy(gameObject);
        }
        if (!Rotation)
        {
            if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Water")
                this.gameObject.tag = "Untagged";
        }
        if (Rotation)
        {
            if (collision.gameObject.tag == "Water")
                this.gameObject.tag = "Untagged";
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        
            if (collision.gameObject.tag == "Ground")
            {
                this.gameObject.tag = "Boulder";
                rb.constraints = RigidbodyConstraints2D.None;
            }

        
    }

    
}
