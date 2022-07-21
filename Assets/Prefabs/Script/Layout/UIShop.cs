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
        Instance = (UIShop)this;
        canvas = GetComponent<Canvas>();
        canvas.sortingOrder = 12;
        this.gameObject.SetActive(false);
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
        SoundManager.PlaySound("click");
        if (UIHome.Instance != null)
        {
            this.gameObject.SetActive(false);
            UIHome.Instance.gameObject.SetActive(true);
        }
        else if (UIManager.Instance.list.Count > 4)
        {
            this.gameObject.SetActive(false);
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

    private void OnDisable()
    {
        btnBack.onClick.RemoveAllListeners();
        btnBack2.onClick.RemoveAllListeners();
    }
}
