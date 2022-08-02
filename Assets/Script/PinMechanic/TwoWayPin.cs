using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoWayPin : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject pos1, pos2;
    [SerializeField] float speed, x, y;
    Collider2D col;
    LevelComplete myScript;
    UIBackground myBackground;
    bool allowMoved;
    public Rigidbody2D rb;
    [Header("TwoWayPin_Direction")]
    [Tooltip("1 - Horizontal, 2 - Vertical, 3 - Diagonal")]
    [SerializeField] int direction;
    Vector2 previousTouchPos;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        myScript = FindObjectOfType<LevelComplete>();
        myBackground = FindObjectOfType<UIBackground>();
        switch (direction)
        {
            case 1:
                {
                    rb.constraints = RigidbodyConstraints2D.FreezePositionY;
                }
                break;

            case 2:
                {
                    rb.constraints = RigidbodyConstraints2D.FreezePositionX;
                }
                break;
                
        }
    }

    private void Update()
    {
        switch (direction)
        {
            case 1:

                if (transform.position.x - pos2.transform.position.x < 0)
                {
                    transform.position = new Vector2(pos2.transform.position.x, transform.position.y);
                }

                if (transform.position.x - pos1.transform.position.x > 0)
                {
                    transform.position = new Vector2(pos1.transform.position.x - 0.001f, transform.position.y);
                }
                break;
            case 2:
                if (transform.position.y - pos2.transform.position.y > 0)
                {

                    transform.position = new Vector2(transform.position.x, pos2.transform.position.y);
                }

                if (transform.position.y - pos1.transform.position.y < 0)
                {
                    transform.position = new Vector2(transform.position.x, pos1.transform.position.y);
                }
                break;

        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (Input.touchCount > 0 && !myScript.check || Input.touchCount > 0 && !myScript.check && !myBackground.isPause)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
            x = touchPos.x - previousTouchPos.x;
            y = touchPos.y - previousTouchPos.y;
            if (touch.phase == TouchPhase.Began)
            {
                Collider2D touchColl = Physics2D.OverlapPoint(touchPos);
                previousTouchPos = touchPos;
                if (col == touchColl)
                {
                    allowMoved = true;
                }
            }
            if (touch.phase == TouchPhase.Moved)
            {
                if (allowMoved)
                {
                    switch (direction)
                    {
                        case 1:
                            /*rb.velocity = new Vector2(x * Time.deltaTime * 80, pos1.transform.position.y);*/
                            rb.velocity = new Vector2(x * 80f * Time.smoothDeltaTime, pos1.transform.position.y);
                            break;
                        case 2:                          
                            rb.velocity = new Vector2(pos1.transform.position.x, y * 80f * Time.smoothDeltaTime);                   
                            break;
                        case 3:
                            if (Mathf.Abs(Vector2.Distance(touchPos, transform.position)) > 0.5f)
                            {
                                if (touchPos.x - previousTouchPos.x > 0)
                                {
                                    rb.velocity = new Vector2((pos1.transform.position.x - pos2.transform.position.x) * Time.deltaTime, (pos1.transform.position.y - pos2.transform.position.y) * Time.deltaTime);
                                    return;
                                }
                                if (touchPos.x - previousTouchPos.x < 0)
                                {
                                    rb.velocity = new Vector2((pos2.transform.position.x - pos1.transform.position.x) * Time.deltaTime, (pos2.transform.position.y - pos1.transform.position.y) * Time.deltaTime);
                                    return;
                                }
                            }
                            break;
                    }
                }
            }

            if(touch.phase == TouchPhase.Stationary)
            {
                rb.velocity = Vector2.zero;
                previousTouchPos = touchPos;
            }

            if (touch.phase == TouchPhase.Ended)
            {
                allowMoved = false;
                rb.velocity = Vector2.zero;
                
            }

        }

        switch (direction)
        {
            case 3:
                if (transform.position.x - pos2.transform.position.x < 0 && transform.position.y - pos2.transform.position.y > 0)
                {

                    rb.velocity = Vector2.zero;
                    transform.position = pos2.transform.position;
                }

                if (transform.position.x - pos1.transform.position.x > 0 && transform.position.y - pos1.transform.position.y < 0)
                {

                    rb.velocity = Vector2.zero;
                    transform.position = new Vector2(pos1.transform.position.x - 0.001f, pos1.transform.position.y + 0.001f);
                }
                break;
            case 1:

                if (transform.position.x - pos2.transform.position.x < 0)
                {
                    rb.velocity = Vector2.zero;
                    rb.MovePosition(new Vector2(pos2.transform.position.x, transform.position.y));
                }

                if (transform.position.x - pos1.transform.position.x > 0)
                {
                    rb.velocity = Vector2.zero;
                    rb.MovePosition(new Vector2(pos1.transform.position.x - 0.001f, transform.position.y));
                }
                break;
            case 2:
                if (transform.position.y - pos2.transform.position.y > 0)
                {

                    rb.velocity = Vector2.zero;
                    rb.MovePosition(new Vector2(transform.position.x, pos2.transform.position.y));
                }

                if (transform.position.y - pos1.transform.position.y < 0)
                {
                    rb.velocity = Vector2.zero;
                    rb.MovePosition(new Vector2(transform.position.x, pos1.transform.position.y));
                }
                break;

        }

    }

    

}
