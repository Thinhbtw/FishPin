using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoWayPin : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject pos1, pos2;
    [SerializeField] float speed;
    Collider2D col;
    LevelComplete myScript;
    UIBackground myBackground;
    bool allowMoved;
    public Rigidbody2D rb;
    Vector2 defaultpos;
    [Header("TwoWayPin_Direction")]
    [Tooltip("1 - Horizontal, 2 - Vertical, 3 - Diagonal")]
    [SerializeField] int direction;
    Vector2 previousTouchPos;
    public Vector3 directionToCaculate { get; private set; }
    public Vector3 newPosition {  get; private set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        myScript = FindObjectOfType<LevelComplete>();
        myBackground = FindObjectOfType<UIBackground>();
        defaultpos = transform.position;
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


    // Update is called once per frame
    private void Update()
    {
        if (Input.touchCount > 0 && !myScript.check || Input.touchCount > 0 && !myScript.check && !myBackground.isPause)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
            
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
                            if (Mathf.Abs(Vector2.Distance(touchPos, previousTouchPos)) > 0.5f)
                                rb.velocity = new Vector2((touchPos.x - previousTouchPos.x)*2, pos1.transform.position.y);
                            break;
                        case 2:
                            if (Mathf.Abs(Vector2.Distance(touchPos, previousTouchPos)) > 0.5f)
                                rb.velocity = new Vector2(pos1.transform.position.x, (touchPos.y - previousTouchPos.y) * 2);                        
                            break;
                        case 3:
                            if (Mathf.Abs(Vector2.Distance(touchPos, previousTouchPos)) > 0.5f)
                            {
                                if (touchPos.x - previousTouchPos.x > 0)
                                {
                                    rb.velocity = new Vector2((pos1.transform.position.x - pos2.transform.position.x) * speed, (pos1.transform.position.y - pos2.transform.position.y) * speed);
                                    return;
                                }
                                if (touchPos.x - previousTouchPos.x < 0)
                                {
                                    rb.velocity = new Vector2((pos2.transform.position.x - pos1.transform.position.x) * speed, (pos2.transform.position.y - pos1.transform.position.y) * speed);
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
            }

            if (touch.phase == TouchPhase.Ended)
            {
                allowMoved = false;
                previousTouchPos = touchPos;
                rb.velocity = Vector2.zero;
                
            }

        }
        switch (direction)
        {
            case 1:

                if (transform.position.x - pos2.transform.position.x < 0)
                {
                    rb.velocity = Vector2.zero;
                    transform.position = new Vector2(pos2.transform.position.x, transform.position.y);
                }

                if (transform.position.x - pos1.transform.position.x > 0)
                {
                    rb.velocity = Vector2.zero;
                    transform.position = new Vector2(pos1.transform.position.x - 0.001f, transform.position.y);
                }
                break;
            case 2:
                if (transform.position.y - pos2.transform.position.y > 0)
                {
                    
                    rb.velocity = Vector2.zero;
                    transform.position = new Vector2(transform.position.x, pos2.transform.position.y);
                }

                if (transform.position.y - pos1.transform.position.y < 0)
                {
                    rb.velocity = Vector2.zero;
                    transform.position = new Vector2(transform.position.x, pos1.transform.position.y);
                }
                break;

            

        }
    }

    private void FixedUpdate()
    {
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
        }
    }
}
