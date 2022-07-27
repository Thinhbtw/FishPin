using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullPin : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Animator anima;
    Collider2D col;
    LevelComplete myScript;
    UIBackground myBackground;

    void Start()
    {
        col = GetComponent<Collider2D>();
        myScript = FindObjectOfType<LevelComplete>();
        myBackground = FindObjectOfType<UIBackground>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Blade")
        {
            anima.Play(StaticClass.OneTimePin_clip);
            Destroy(this.gameObject, 0.5f);
        }

    }


    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && !myScript.check && !myBackground.isPause)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
            if (touch.phase == TouchPhase.Began)
            {
                Collider2D touchColl = Physics2D.OverlapPoint(touchPos);
                if (col == touchColl)
                {
                    anima.Play(StaticClass.OneTimePin_clip);
                    Destroy(this.gameObject, 0.5f);
                }
            }

        }
    }

    
}
