using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcePlatHealth : MonoBehaviour
{
    int count;
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Lava")
            count++;
        if(collision.gameObject.tag == "Bomb" || collision.gameObject.tag == "Boulder")
            Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Lava" )
            count++;
    }

    private void Update()
    {
        if (count > 2)
            Invoke("DestroyPlat", 2);
    }

    public void DestroyPlat()
    {
        Destroy(this.gameObject);
    }
}
