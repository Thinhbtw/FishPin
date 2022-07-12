using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] Animator anima;
    public bool isDed, onGround;
    int curHealth;
    LevelComplete myScript;

    private void Awake()
    {
        myScript = GameObject.FindObjectOfType<LevelComplete>();
        isDed = false;
        curHealth = StaticClass.startHealth;
        onGround = true;
        IgnoreColision(false);
        Physics2D.IgnoreLayerCollision(9, 9);
    }

    private void Start()
    {
        anima.SetLayerWeight(1, 0);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Fish")
        {
            anima.SetLayerWeight(1, 1);
        }
        if (collision.gameObject.tag == "GroundCheck")
            onGround = false;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayClip("BossParent", StaticClass.Boss_clipDie, StaticClass.Boss_clipExplo, collision);
        PlayClip("SnakeParent", StaticClass.Snake_clipDie, StaticClass.Snake_clipExplo, collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "GroundCheck")
            onGround = false;

    }

    void PlayClip(string bossName, string clipDie, string clipExplo, Collision2D collision)
    {
        if (this.gameObject.name.Contains(bossName))
        {
            if (collision.gameObject.tag == "Spike" || collision.gameObject.tag == "Poison" || collision.gameObject.tag == "Boulder" || collision.gameObject.tag == "Untagged" || collision.gameObject.tag == "IceBlock")
            {
                anima.Play(clipDie);
                isDed = true;
                IgnoreColision(true);
                StartCoroutine(Destroy());
            }

            if (collision.gameObject.tag == "Bomb")
            {
                anima.Play(clipExplo);
                isDed = true;
                IgnoreColision(true);
                StartCoroutine(Destroy());

            }

            if (curHealth > 0)
            {
                if (collision.gameObject.tag == "Lava")
                {
                    Destroy(collision.gameObject);
                    anima.SetLayerWeight(2, 1);
                    curHealth = Mathf.Clamp(curHealth - 1, 0, StaticClass.startHealth);
                }
                else
                    anima.SetLayerWeight(2, 0);

            }
            if (curHealth == 0)
            {
                isDed = true;
                anima.Play(clipDie);
                StartCoroutine(Destroy());
            }

            if (collision.gameObject.tag == "Fish")
            {
                anima.SetLayerWeight(1, 1);
            }
        }
    }
    void IgnoreColision(bool check)
    {
        Physics2D.IgnoreLayerCollision(9, 11, check);
        Physics2D.IgnoreLayerCollision(9, 9, check);
        Physics2D.IgnoreLayerCollision(9, 8, check);
        Physics2D.IgnoreLayerCollision(9, 13, check);
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(0.5f);
        this.transform.GetChild(0).gameObject.SetActive(false);
        this.gameObject.GetComponent<BossMove>().enabled = false;
        this.gameObject.GetComponent<Boss>().enabled = false;
        myScript.bossIsDed = true;
        Destroy(this.gameObject);
        yield break;
    }

}
