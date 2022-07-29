using DailyQuestSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setIcon : MonoBehaviour
{
    [SerializeField] Image Icon;

    public void setAchieveIcon(Sprite iconImg)
    {
        Icon.sprite = iconImg;
    }
}
