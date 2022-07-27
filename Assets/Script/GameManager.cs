using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    

    private static GameManager instance;
    public static GameManager Instance => instance;

    private void Awake()
    {
        if(Instance == null)
            instance = this;
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }


    

}
