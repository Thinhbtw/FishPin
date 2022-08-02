using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelProgressBar : MonoBehaviour
{
    public Slider progressBar;
    UIGameplay uiGameplay;
    public GameObject progress;
    int num;
    public Sprite[] image;
    [SerializeField] Animator cupAnima;
    [SerializeField] GameObject plus50;
    bool hasAddCoin;

    // Start is called before the first frame update

    private void Awake()
    {
        uiGameplay = FindObjectOfType<UIGameplay>();
        plus50.SetActive(false);
        hasAddCoin = false;
    }

    private void OnEnable()
    {
        progressBar.value = uiGameplay.levelAt % 5;
        
        if(progressBar.value % 5 == 0)
        {
            progress.transform.GetChild(0).GetComponent<Image>().sprite = image[0];
            progress.transform.GetChild(1).GetComponent<Image>().sprite = image[0];
            progress.transform.GetChild(2).GetComponent<Image>().sprite = image[0];
            progress.transform.GetChild(3).GetComponent<Image>().sprite = image[0];
        }

        num = uiGameplay.levelAt % 5;
        for(int i = 0; i < num; i++)
        {
            progress.transform.GetChild(i).GetComponent<Image>().sprite = image[1];
        }
        
    }

    private void Update()
    {
        if(uiGameplay == null)
            uiGameplay = FindObjectOfType<UIGameplay>();

        if (progressBar.value < 4 && uiGameplay.levelAt % 5 != 0)
        {
            progress.transform.GetChild((uiGameplay.levelAt % 5 - 1)).GetComponent<Image>().sprite = image[1];
            progressBar.value = Mathf.MoveTowards(progressBar.value, uiGameplay.levelAt % 5, Time.deltaTime * 1);

        }
        else if(progressBar.value == 4 && uiGameplay.levelAt % 5 == 0)
        {
            if (!PlayerPrefs.GetString("LevelComplete").Contains((uiGameplay.levelAt / 5).ToString()) && !hasAddCoin)
            {
                plus50.SetActive(true);
                GameData.AddCoin(200);
                hasAddCoin = true;
            }
        }
     
    }

}