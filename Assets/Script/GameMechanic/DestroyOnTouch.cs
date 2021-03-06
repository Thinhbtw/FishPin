using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTouch : MonoBehaviour
{
    public Rigidbody2D rb;
    int i; 
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreLayerCollision(11, 14);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Water")
        {
            SoundManager.PlaySound2("fizz");
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
         
        }
    }

}
