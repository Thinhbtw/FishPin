using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dailyCheckin : MonoBehaviour
{
    public static dailyCheckin Instance;

    [SerializeField] GameObject general;
    [SerializeField] GameObject day7;



    private void Awake()
    {
        Instance = (dailyCheckin)this;
    }
    private void OnEnable()
    {
        for(int i = 0; i < general.transform.childCount; i++)
        {

            /*general.transform.GetChild(i).GetComponent<>*/
        }
    }
}
