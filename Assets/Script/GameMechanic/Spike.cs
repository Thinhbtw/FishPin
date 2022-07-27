using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Physics2D.IgnoreLayerCollision(8, 6);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
