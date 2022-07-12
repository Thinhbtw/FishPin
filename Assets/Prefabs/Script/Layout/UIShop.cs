using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShop : MonoBehaviour
{
    public static UIShop Instance;
    [SerializeField] Button btnBack, btnBack2;
    public Canvas canvas;
    // Start is called before the first frame update

    void Awake()
    {
        GameData.AddGems(1005);
        GameData.AddCoin(2050);
        canvas = GetComponent<Canvas>();
        Instance = (UIShop)this;
    }

    private void OnEnable()
    {
        btnBack.onClick.AddListener(() =>
        {
            ClickToExit();
        });

        btnBack2.onClick.AddListener(() =>
        {
            ClickToExit();
        });
    }

    void ClickToExit()
    {
        if (UIManager.Instance.list.Count == 2)
        {
            UIManager.Instance.LoadPreviousDialog();
            UIHome.Instance.gameObject.SetActive(true);
        }
        else if (UIManager.Instance.list.Count > 4)
        {
            UIManager.Instance.RemoveFromListDialog(this.gameObject);
            UISettingIngame.Instance.Pause();
            UISettingIngame.Instance.gameObject.SetActive(true);
            UIBackground.Instance.gameObject.SetActive(true);
            UIGameplay.Instance.gameObject.SetActive(true);
            UiEnd.Instance.gameObject.SetActive(true);

        }
    }
    private void Update()
    {
        GameSharedUI.instance.UpdateCoinsUIText();
        GameSharedUI.instance.UpdateGemsUIText();
    }
}
