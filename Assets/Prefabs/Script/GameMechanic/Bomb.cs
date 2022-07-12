using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb;
    GameObject curPortal;
    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Spike" || collision.gameObject.tag == "Boss" || collision.gameObject.tag == "Boulder"
            || collision.gameObject.tag == "IcePlat" || collision.gameObject.tag == "Untagged" || collision.gameObject.tag == "Lava"
            || collision.gameObject.tag == "Fish") 
        {
            anim.Play(StaticClass.Bomb_Explo);
            Handheld.Vibrate();
            StartCoroutine(Destroy());
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            rb.constraints = RigidbodyConstraints2D.None;
        }       
    }


    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(0.35f);
        Destroy(this.gameObject);
        yield break;
    }

    public void ConnectRopeEnd(Rigidbody2D rb)
    {
        HingeJoint2D joint = gameObject.AddComponent<HingeJoint2D>();
        joint.autoConfigureConnectedAnchor = false;
        joint.connectedBody = rb; 
        joint.anchor = Vector2.zero;
        joint.connectedAnchor = new Vector2(1f, 0);
    }
}
