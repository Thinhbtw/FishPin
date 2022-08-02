using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject UIHome, UISetting, UIGameplay, UISettingIngame, UIEnd, UIShop, UILevel, UIAchivement;
    private static UIManager instance;
    public static UIManager Instance => instance;
    public List<GameObject> listLevel;
    public List<GameObject> list;
    private void Awake()
    {
        /*PlayerPrefs.DeleteAll();*/
        list = new List<GameObject>();
        if (Instance == null)
            instance = this;
        listLevel = new List<GameObject>(Resources.LoadAll<GameObject>("Level"));
        var UI = Instantiate(UIHome, transform);
        list.Add(UI);
        if (!PlayerPrefs.HasKey("SaveSettings"))
        {
            PlayerPrefs.SetString("SaveSettings", "024");
        }
    }

    public void AddToListDialog(GameObject UI)
    {

        var Dialog = Instantiate(UI, transform);
        list.Add(Dialog);
        Dialog.GetComponent<Canvas>().sortingOrder = list.Count - 1;
        if (list.Count > 1)
            list[0].gameObject.SetActive(false);
    }

   public void RemoveFromListDialog(GameObject UI)
    {
        Destroy(UI);
        list.Remove(UI);

    }

    public void LoadPreviousDialog()
    {
        for (int i = 0; i < list.Count; i++)
        {

            int num = i;
            if (num == list.Count - 1)
            {
                list[num - 1].gameObject.SetActive(true);
                Destroy(list[num].gameObject);
                RemoveFromListDialog(list[num].gameObject);
            }
            if (list.Count == 1)
            {
                break;
            }
        }
    }

    public void GoBackToHome()
    {
        foreach(GameObject UI in list)
        {
            Destroy(UI);
        }
        list.Clear();
        
    }

    public int ListCount()
    {
        return list.Count;
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey("SelectedLevel");
    }

}
