using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WInCondition : MonoBehaviour
{
    public int count;
    LevelComplete myScript;
    // Start is called before the first frame update
    void Awake()
    {
        myScript = GameObject.FindObjectOfType<LevelComplete>();
        count = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Water" && !myScript.isComplete && !myScript.isDed)
            count++;

            
    }


}
