using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class achiveNotif : MonoBehaviour
{
    [SerializeField] GameObject notification;
    [SerializeField] RewardDatabases achivement;

    // Update is called once per frame
    void Update()
    {
        if (notifcheck())
        {
            notification.SetActive(true);
        }
        else
        {
            notification.SetActive(false);
        }
    }
    bool notifcheck()
    {
        for (int i = 0; i < achivement.rewardsCount; i++)
        {
            if (PlayerPrefs.GetInt(achivement.GetQuest(i).title) == INSTANCE.claimAble)
            {
                return true;
            }
        }
        return false;
    }
}
