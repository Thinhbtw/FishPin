using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelComplete : MonoBehaviour
{
    [SerializeField] GameObject Confetti;

    public bool check, isComplete, isDed, bossIsDed;

    private static LevelComplete instance;
    public static LevelComplete Instance => instance;

    private void Awake()
    {
        if (Instance == null)
            instance = this;
        
    }

    public void setGameEnd(bool isWin)
    {
        check = true;
        isComplete = isWin;
        if(isWin == true)
        {
            SoundManager.PlaySound("confeti");
            Confetti.SetActive(true);
        }
        
        StartCoroutine(UiEnd.Instance.WaitPanel(isWin));
    }

    private void OnEnable()
    {

        
        Confetti = transform.GetChild(0).gameObject;
        bossIsDed = false;
        
        check = false;
        isComplete = false;
        isDed = false;
        bossIsDed = false ;
        Confetti.SetActive(false);

    }

}
