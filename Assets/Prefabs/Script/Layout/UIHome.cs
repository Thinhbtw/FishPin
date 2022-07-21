using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHome : MonoBehaviour
{
    [SerializeField] Button btnStart, btnSetting, btnShop, btnAchivement, btnDropMenu, btnLevel;

    public static UIHome Instance;
    [SerializeField] GameObject panel;
    bool isOn;
    [SerializeField] Text levelText;
    private void Awake()
    {
        
        Instance = (UIHome)this;
        if (PlayerPrefs.HasKey("SelectedLevel"))
        {
            btnLevel.interactable = true;
        }
        else if (!PlayerPrefs.HasKey("SelectedLevel") && PlayerPrefs.HasKey("Progress"))
        {
            btnLevel.interactable = true;
        }
        else
            btnLevel.interactable = false;
    }

    private void Start()
    {
        if (btnDropMenu != null)
            btnDropMenu.onClick.AddListener(() =>
            {
                SoundManager.PlaySound("click");
                isOn = !isOn;
                panel.gameObject.SetActive(isOn);
            });

        
    }


    private void OnEnable()
    {
        if (btnLevel != null)
            btnLevel.onClick.AddListener(() =>
            {
                SoundManager.PlaySound("click");
                if (UILevel.Instance == null)
                {
                    UIManager.Instance.AddToListDialog(UIManager.Instance.UILevel);
                }
                else
                {
                    UILevel.Instance.gameObject.SetActive(true);
                }
            });
        if (btnStart != null)
            btnStart.onClick.AddListener(() =>
            {
                SoundManager.PlaySound("click");
                if (UIGameplay.Instance == null && UiEnd.Instance == null)
                {
                    UIManager.Instance.RemoveFromListDialog(this.gameObject);
                    UIManager.Instance.AddToListDialog(UIManager.Instance.UIGameplay);
                    UIManager.Instance.AddToListDialog(UIManager.Instance.UIEnd);
                    UIManager.Instance.AddToListDialog(UIManager.Instance.UILevel);
                    UILevel.Instance.gameObject.SetActive(false);
                    UIBackground.Instance.gameObject.SetActive(true);
                    UiEnd.Instance.gameObject.SetActive(true);
                    UIGameplay.Instance.gameObject.SetActive(true);
                    UiEnd.Instance.canvas.sortingOrder = 10;
                    

                }
                else
                {
                    UiEnd.Instance.gameObject.SetActive(true);
                    UIGameplay.Instance.gameObject.SetActive(true);
                    UIBackground.Instance.gameObject.SetActive(true);
                }
                
            }
            );
        if(btnSetting != null)
        {
            btnSetting.onClick.AddListener(() =>
            {
                SoundManager.PlaySound("click");
                if (UISetting.Instance == null)
                {
                    UIManager.Instance.AddToListDialog(UIManager.Instance.UISetting);
                }
                else
                {
                    UISetting.Instance.gameObject.SetActive(true);
                }
            });
        }

        if (btnShop != null)
            btnShop.onClick.AddListener(() =>
            {
                SoundManager.PlaySound("click");
                if (UIShop.Instance != null)
                {
                    gameObject.SetActive(false);
                    UIShop.Instance.gameObject.SetActive(true);
                }
            });

        if (btnAchivement != null)
            btnAchivement.onClick.AddListener(() =>
            {
                SoundManager.PlaySound("click");
                if (achivementManager.instance == null)
                {
                    gameObject.SetActive(false);
                    UIManager.Instance.AddToListDialog(UIManager.Instance.UIAchivement);
                }
            });
        
    }

    private void OnDisable()
    {
        btnAchivement.onClick.RemoveAllListeners();
        btnShop.onClick.RemoveAllListeners();
        btnSetting.onClick.RemoveAllListeners();
        btnStart.onClick.RemoveAllListeners();
        btnLevel.onClick.RemoveAllListeners();
    }
}
