using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILevel : MonoBehaviour
{
    [SerializeField]Button btnBack;
    [SerializeField] GameObject gridButton;
    [SerializeField] GameObject btn;
    public static UILevel Instance;
    [SerializeField] string skip;
    private void Awake()
    {
        Instance = (UILevel)this;
        for (int i = 0; i < UIManager.Instance.listLevel.Count / 5; i++)
        {
            int num = i;
            var button = (GameObject)Instantiate(btn, gridButton.transform);
            if(i + 1 < 10)
            {
                button.name = "Level" + (i + 1);
                button.GetComponentInChildren<Text>().text = "Level 0" + (i + 1);
                button.GetComponent<Button>().onClick.AddListener(() => LoadLevel(num));
            }
            else
            { 
            button.name = "Level" + (i + 1);
            button.GetComponentInChildren<Text>().text = "Level " + (i + 1);
            button.GetComponent<Button>().onClick.AddListener(() => LoadLevel(num));
            }

        }
    }

    void LoadLevel(int value)
    {
        if (UIGameplay.Instance != null && UIHome.Instance == null)
        {
            UIGameplay.Instance.levelAt = (value * 5);
            var lvl = Instantiate(UIManager.Instance.listLevel[UIGameplay.Instance.levelAt], UIGameplay.Instance.levelField.transform);
            UIGameplay.Instance.Level.Add(lvl);
            Destroy(UIGameplay.Instance.Level[UIGameplay.Instance.Level.Count - UIGameplay.Instance.Level.Count]);
            UIGameplay.Instance.Level.RemoveAt(UIGameplay.Instance.Level.Count - UIGameplay.Instance.Level.Count);
            UiEnd.Instance.pannel.SetActive(false);



            UIManager.Instance.LoadPreviousDialog();
            UIGameplay.Instance.gameObject.SetActive(true);
            UiEnd.Instance.gameObject.SetActive(true);
            UIBackground.Instance.gameObject.SetActive(true);
            PlayerPrefs.SetInt("SelectedLevel", value);
        }
        else
        {
            UIManager.Instance.AddToListDialog(UIManager.Instance.UIGameplay);
            UIManager.Instance.AddToListDialog(UIManager.Instance.UIEnd);
            UIManager.Instance.AddToListDialog(UIManager.Instance.UILevel);
            this.gameObject.SetActive(false);
            UIBackground.Instance.gameObject.SetActive(true);
            UiEnd.Instance.canvas.sortingOrder = 10;
            UIManager.Instance.RemoveFromListDialog(this.gameObject);
            UIManager.Instance.RemoveFromListDialog(UIHome.Instance.gameObject);


            UIGameplay.Instance.levelAt = (value * 5);
            var lvl = Instantiate(UIManager.Instance.listLevel[UIGameplay.Instance.levelAt], UIGameplay.Instance.levelField.transform);
            UIGameplay.Instance.Level.Add(lvl);
            Destroy(UIGameplay.Instance.Level[UIGameplay.Instance.Level.Count - UIGameplay.Instance.Level.Count]);
            UIGameplay.Instance.Level.RemoveAt(UIGameplay.Instance.Level.Count - UIGameplay.Instance.Level.Count);
            UiEnd.Instance.pannel.SetActive(false);



            UIManager.Instance.LoadPreviousDialog();
            UIGameplay.Instance.gameObject.SetActive(true);
            UiEnd.Instance.gameObject.SetActive(true);
            UIBackground.Instance.gameObject.SetActive(true);
            PlayerPrefs.SetInt("SelectedLevel", value);
        }
    }

    private void OnEnable()
    {
        if (btnBack != null)
            btnBack.onClick.AddListener(() =>
            {
                if (UIGameplay.Instance != null && UIHome.Instance == null)
                {
                    this.gameObject.SetActive(false);
                    UIGameplay.Instance.gameObject.SetActive(true);
                    UiEnd.Instance.gameObject.SetActive(true);
                    UIBackground.Instance.gameObject.SetActive(true);
                }
                else if(UIHome.Instance != null && UIGameplay.Instance == null)
                {
                    UIManager.Instance.LoadPreviousDialog();
                    UIHome.Instance.gameObject.SetActive(true);
                }

            });
        for (int i = 0; i < UIManager.Instance.listLevel.Count / 5; i++)
        {
            int num = i;
            if (PlayerPrefs.GetInt("Progress") / 5 < num)
            {
                gridButton.transform.GetChild(i).GetComponent<Button>().interactable = false;
                gridButton.transform.GetChild(i).GetChild(0).GetComponent<Text>().color = new Color32(255, 255, 255, 150);
                gridButton.transform.GetChild(i).GetChild(0).GetComponent<Shadow>().enabled = false;
            }
            else
            {
                gridButton.transform.GetChild(i).GetComponent<Button>().interactable = true;
                gridButton.transform.GetChild(i).GetChild(0).GetComponent<Shadow>().enabled = true;
                gridButton.transform.GetChild(i).GetChild(0).GetComponent<Text>().color = new Color32(255, 255, 255, 255);
            }
            if (PlayerPrefs.GetString("LevelSkip").Contains((num - 1).ToString()))
            {
                gridButton.transform.GetChild(i - 1).GetChild(0).GetComponent<Text>().color = new Color32(241, 122, 53, 255);
            }
            if (PlayerPrefs.GetString("LevelComplete").Contains((num).ToString()))
            {
                gridButton.transform.GetChild(num).GetChild(0).GetComponent<Text>().color = new Color32(91, 255, 59, 255);
            }


        }


    }


}
