using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishTele : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject curPortal;
    Rigidbody2D rb;
    [SerializeField]bool check;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        check = false;
    }

    private void Start()
    {
        check = false ;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Portal1" && !check || collision.gameObject.name == "Portal2" && !check)
        {
            curPortal = collision.gameObject;
            check = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Portal1" || collision.gameObject.name == "Portal2")
        {
            if (collision.gameObject == curPortal)
            {
                check = true;
                curPortal = null;
                StartCoroutine(ResetTele());
            }
        }
    }

    IEnumerator ResetTele()
    {
        yield return new WaitForSeconds(0.5f);
        check = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (curPortal != null)
        {
            transform.position = curPortal.GetComponent<Teleporter>().GetDestination().position;
            
        }
    }
}
