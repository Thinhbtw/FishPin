using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishHealth : MonoBehaviour
{
    [SerializeField]Animator animator;
    [SerializeField]int curHealth;
    [SerializeField] WInCondition wInCondition;
    public bool Winning, runProgress;
    Rigidbody2D rb;
    LevelComplete myScript;
    UIGameplay uiGameplay;

    private static FishHealth instance;
    public static FishHealth Instance => instance;


    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
            instance = this;

        curHealth = StaticClass.startHealth;
        rb = GetComponent<Rigidbody2D>();
        Winning = false;        
    }

    private void OnEnable()
    {
        myScript = GameObject.FindObjectOfType<LevelComplete>();
        uiGameplay = FindObjectOfType<UIGameplay>();
        
    }

    void Start()
    {
        Physics2D.IgnoreLayerCollision(10, 6);
        Physics2D.IgnoreLayerCollision(10, 7, false);
        Physics2D.IgnoreLayerCollision(10, 8, false);
        Physics2D.IgnoreLayerCollision(10, 9, false);
        Physics2D.IgnoreLayerCollision(10, 11, false);
        Physics2D.IgnoreLayerCollision(10, 12, false);
        Physics2D.IgnoreLayerCollision(10, 13, false);
        animator.SetLayerWeight(1, 0);


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (curHealth > 0)
        {
            if (collision.gameObject.tag == "Lava")
            {
                animator.SetLayerWeight(2, 1);
                curHealth = Mathf.Clamp(curHealth - 1, 0, StaticClass.startHealth);
                Destroy(collision.gameObject);
                StartCoroutine(ResetIdle());

            }
        }

        if (collision.gameObject.tag == "Boss" || collision.gameObject.tag == "Spike" || collision.gameObject.tag == "Poison")
        {
            Physics2D.IgnoreLayerCollision(10, 6);
            Physics2D.IgnoreLayerCollision(10, 9);
            myScript.isDed = true;
            animator.SetLayerWeight(1, 0);
            animator.Play(StaticClass.Fish_clipDie);
            myScript.setGameEnd(false);
            if (Winning)
                uiGameplay.levelAt = uiGameplay.levelAt - 1;

        }

        if (collision.gameObject.tag == "Boulder" || collision.gameObject.tag == "IceBlock")
        {
            Physics2D.IgnoreLayerCollision(10, 6);
            SoundManager.PlaySound("bonk");
            myScript.isDed = true;
            animator.SetLayerWeight(1, 0);
            animator.Play(StaticClass.Fish_clipStun);
            myScript.setGameEnd(false);
            if (Winning)
                uiGameplay.levelAt = uiGameplay.levelAt - 1;
        }

        if (collision.gameObject.tag == "Bomb")
        {
            Physics2D.IgnoreLayerCollision(10, 6);
            myScript.isDed = true;
            animator.SetLayerWeight(1, 0);
            animator.Play(StaticClass.Fish_clipBurn);
            myScript.setGameEnd(false);
            if (Winning)
                uiGameplay.levelAt = uiGameplay.levelAt - 1;
        }
    }

    public IEnumerator ProgressBar()
    {
        yield return new WaitForSeconds(3.35f);
        uiGameplay.levelAt++;
    }

    IEnumerator ResetIdle()
    {
        yield return new WaitForSeconds(1f);
        animator.SetLayerWeight(2, 0);
        yield break;
    }

    // Update is called once per frame
    void Update()
    {

        if (wInCondition.count >= StaticClass.WinningCondition && !myScript.isDed)
        {
            curHealth = 100;
            runProgress = true;
            Winning = true;
            Physics2D.IgnoreLayerCollision(10, 7);
            Physics2D.IgnoreLayerCollision(10, 8);
            Physics2D.IgnoreLayerCollision(10, 9);
            Physics2D.IgnoreLayerCollision(10, 11);
            Physics2D.IgnoreLayerCollision(10, 12);
            Physics2D.IgnoreLayerCollision(10, 13);
            animator.Play(StaticClass.Fish_clipWin);
            
            myScript.setGameEnd(true);
            if (runProgress)
            {         
                StartCoroutine(ProgressBar());
                runProgress = false;
            }
            wInCondition.count = 0;
        }

        if (curHealth == 0)
        {

            Physics2D.IgnoreLayerCollision(10, 11);
            myScript.isDed = true;
            animator.Play(StaticClass.Fish_clipBurn);
            if (!myScript.check)
            {
                myScript.setGameEnd(false);
            }
            animator.SetLayerWeight(1, 0);
        }
        
    }
}