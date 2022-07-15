using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTouch1 : MonoBehaviour
{
    public Rigidbody2D rb;
    [SerializeField] float power;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreLayerCollision(6, 7);
        Physics2D.IgnoreLayerCollision(6, 9);
        Physics2D.IgnoreLayerCollision(6, 10);
        Physics2D.IgnoreLayerCollision(6, 14);
        

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Lava")
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
        
    }

}
