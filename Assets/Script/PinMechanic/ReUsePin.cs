using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReUsePin : MonoBehaviour
{
    [SerializeField]Animator anima;
    public bool isLeft, isRight;
    Collider2D col;
    LevelComplete myScript;
    UIBackground myBackground;

    // Start is called before the first frame update
    void Start()
    {
        isRight = true;
        col = GetComponent<Collider2D>();
        myScript = FindObjectOfType<LevelComplete>();
        myBackground = FindObjectOfType<UIBackground>();
    }

    IEnumerator CheckLeft()
    {
        yield return new WaitForSeconds(0.4f);
        isLeft = true;
        isRight = false;
    }

    IEnumerator CheckRight()
    {
        yield return new WaitForSeconds(0.4f);
        isLeft = false;
        isRight = true;
    }

    // Update is called once per frame

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Blade")
        {
            if (isRight)
            {
                anima.Play(StaticClass.ReusePin_clipPull);
                StartCoroutine(CheckLeft());
            }
            if (isLeft)
            {
                anima.Play(StaticClass.ReusePin_clipPush);
                StartCoroutine(CheckRight());
            }
        }
    }

    void Update()
    {
        if (Input.touchCount > 0 && !myScript.check && !myBackground.isPause)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
            if (touch.phase == TouchPhase.Began)
            {
                Collider2D touchColl = Physics2D.OverlapPoint(touchPos);
                if (col == touchColl && isRight)
                {
                    anima.Play(StaticClass.ReusePin_clipPull);
                    StartCoroutine(CheckLeft());
                }
                if (col == touchColl && isLeft)
                {
                    anima.Play(StaticClass.ReusePin_clipPush);
                    StartCoroutine(CheckRight());
                }
            }

        }
    }
}
