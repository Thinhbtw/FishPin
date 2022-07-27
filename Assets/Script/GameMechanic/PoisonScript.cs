using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonScript : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rgb;
    static bool isTouch;
    float distance, distance2;
    [SerializeField] GameObject fish, boss;
    LevelComplete myScript;

    private void Awake()
    {
        myScript = GameObject.FindObjectOfType<LevelComplete>();
        isTouch = false;
        rgb = GetComponent<Rigidbody2D>();
        fish = FindObjectOfType<FishHealth>().gameObject;
        boss = FindObjectOfType<Boss>().gameObject;
    }

    public Transform getFishtranform()
    {
        return fish.transform;
    }


    public Transform getBossPos()
    {
        return boss.transform;
    }

    private void Start()
    {
        InvokeRepeating("RandomPosition", 1,1);
    }

    void RandomPosition()
    {
        rgb.velocity = Random.insideUnitCircle * 1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Lava" || collision.gameObject.tag == "Water")
        {
            rgb.velocity = Vector3.zero;
            Destroy(collision.gameObject);
        }
        if(collision.gameObject.tag == "Boss" || collision.gameObject.tag == "Fish")
        {
            isTouch = true;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Fish")
            isTouch = true;
    }

    void Update()
    {
        if (isTouch)
            Destroy(this.gameObject);


        if (!myScript.bossIsDed)
        {
            getBossPos();
        }
        getFishtranform();

        distance = Vector2.Distance(transform.position, getFishtranform().position);
        if (distance <= 2f)
            transform.position = Vector2.MoveTowards(transform.position, getFishtranform().position, 0.5f * Time.deltaTime);

        if (!myScript.bossIsDed)
        {
            distance2 = Vector2.Distance(transform.position, getBossPos().position);
            if (distance2 <= 7f)
                transform.position = Vector2.MoveTowards(transform.position, getBossPos().position, 0.5f * Time.deltaTime);
        }


    }
}
