using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameplay : MonoBehaviour
{
    public static UIGameplay Instance;
    public Transform levelField;
    public List<GameObject> Level;
    public bool hasFound, hasNextLevel, stillatDefault;
    public int whichLevel = 0, levelAt, levelPrefs;
    UiEnd uiEnd;
    


    private void Awake()
    {
        Instance = (UIGameplay)this;
                 
        stillatDefault = true;
        levelPrefs = PlayerPrefs.GetInt("Progress2");
        levelAt = levelPrefs;
    }

    public void OnEnable()
    {
        if (PlayerPrefs.HasKey("Progress") == false)
            PlayerPrefs.SetInt("Progress", 0);
        if (PlayerPrefs.HasKey("Progress2") == false)
            PlayerPrefs.SetInt("Progress2", 0);
        hasNextLevel = true;
        if(levelField.transform.childCount < 1)
        {
            var lvl = Instantiate(UIManager.Instance.listLevel[levelPrefs], levelField.transform);
            Level.Add(lvl);

        }
        
    }

    private void Update()
    {
        if(levelAt > levelPrefs)
        {
            PlayerPrefs.SetInt("Progress2", levelAt);
        }
        if (uiEnd == null)
        {
            uiEnd = FindObjectOfType<UiEnd>();
            hasNextLevel = true;
        }

        if (uiEnd.isComplete && hasNextLevel && whichLevel < UIManager.Instance.listLevel.Count - 1)
        {
            var lvl = Instantiate(UIManager.Instance.listLevel[levelAt], levelField.transform);
            Level.Add(lvl);
            Destroy(Level[Level.Count - Level.Count]);
            Level.RemoveAt(Level.Count - Level.Count);
            if (levelAt % 5 == 0 && levelAt != 0)
            {
                if (PlayerPrefs.GetInt("Progress") < levelAt)
                    PlayerPrefs.SetInt("Progress", PlayerPrefs.GetInt("Progress") + 5);
                PlayerPrefs.SetString("LevelSkip", PlayerPrefs.GetString("LevelSkip").Replace(((levelAt - 1) / 5).ToString(), ""));
                PlayerPrefs.SetInt("SelectedLevel", PlayerPrefs.GetInt("SelectedLevel") + 1);
                PlayerPrefs.SetString("LevelComplete", PlayerPrefs.GetString("LevelComplete") + ((levelAt - 1) / 5).ToString());
            }
            uiEnd.isComplete = false;
            hasNextLevel = false;
            
        }

        
    }
}
