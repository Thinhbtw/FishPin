using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    bool isCutting = false;
    Rigidbody2D rb;
    CircleCollider2D circleCollider;
    bool check;
    TrailRenderer trailRenderer;
    LevelComplete myScript;
    UIBackground iBackground;



    private void Awake()
    {
        
        rb = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        trailRenderer = GetComponentInChildren<TrailRenderer>();
        check = true;
        myScript = FindObjectOfType<LevelComplete>();
    }

    private void Start()
    {
        iBackground = FindObjectOfType<UIBackground>();
    }

    private void Update()
    {


        if (Input.touchCount > 0 && !myScript.check && !iBackground.isPause)
        {
            Touch touch = Input.GetTouch(0);
            

            if (touch.phase == TouchPhase.Began && check)
            {
                StartCutting();
            }

            if(touch.phase == TouchPhase.Moved && isCutting)
            {
                ContinueSlice();
            }

            if(touch.phase == TouchPhase.Ended || myScript.check)
            {
                StopCutting();
            }
        }

    }


    private void OnEnable()
    {
        StopCutting();
    }

    private void OnDisable()
    {
        StopCutting();
    }

    void StartCutting()
    {
        rb.position = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        transform.position = rb.position;

        isCutting = true;
        check = false;
        trailRenderer.Clear();
    }

    void StopCutting()
    {
        isCutting = false;
        circleCollider.enabled = false;
        check = true;
        rb.position.Set(0, 0);
    }

    void ContinueSlice()
    {
        circleCollider.enabled = true;
        rb.position = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        
        transform.position = rb.position;

    }


}
