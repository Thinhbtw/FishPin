using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    // Start is called before the first frame update
    public List<TabButtons> tabButtons;
    public Sprite[] tabIdle;
    public Sprite tabActive;
    public TabButtons selectedTab;
    public TabButtons[] buttonArray;
    public List<GameObject> objectsToSwap;
    public int index;
    public Outline[] outlines;

    public void Subscribe(TabButtons button)
    {
        if(tabButtons == null)
        {
            tabButtons = new List<TabButtons>();
        }
        tabButtons.Add(button);
        
    }

    public void OnTabExit(TabButtons button)
    {
        ResetTab();
    }
    public void OnTabSelected(TabButtons button)
    {
        selectedTab = button;
        ResetTab();
        button.outline.effectColor = new Color32(232, 85, 241, 139);
        button.outline.effectDistance = new Vector2(-1.35f,3.99f);
        index = button.transform.GetSiblingIndex();
        for(int i = 0; i < objectsToSwap.Count; i++)
        {
            if(i == index)
            {
                objectsToSwap[i].SetActive(true);
            }
            else
            {
                objectsToSwap[i].SetActive(false);
            }
        }
    }

    public void ResetTab()
    {
        for(int i = 0; i < tabButtons.Count; i++)
        {
            if(selectedTab != null && buttonArray[i] == selectedTab) { continue; }
            buttonArray[i].outline.effectColor = new Color32(0,0,0,80);
            buttonArray[i].outline.effectDistance = new Vector2(-0.76f, 2.64f);
        }
    }
}
