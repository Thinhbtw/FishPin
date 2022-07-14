using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveAround : MonoBehaviour
{
    [SerializeField]RectTransform rectTransform;
     Boss boss;
     BossMove bossMove;
    [SerializeField] FishHealth fish;

    float dirX, moveSpeed;
    Rigidbody2D rb;


    // Start is called before the first frame update
    private void Awake()
    {
        
        boss = GetComponentInParent<Boss>();
        rb = GetComponentInParent<Rigidbody2D>();
        bossMove = GetComponentInParent<BossMove>();
    }

    private void Start()
    {
        dirX = -1f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            dirX *= -1f;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if(boss.onGround && !fish.Winning && !boss.isDed)
            rb.velocity = new Vector2(dirX * StaticClass.Boss_speed, rb.velocity.y);
        if (dirX > 0f && boss.onGround && !bossMove.inRange)
            rectTransform.rotation = Quaternion.Euler(0, 180f, 0);
        else if (dirX < 0f && boss.onGround && !bossMove.inRange)
            rectTransform.rotation = Quaternion.Euler(0, 0f, 0);
        if(fish.Winning)
            rb.velocity = Vector2.zero;

    }
}
