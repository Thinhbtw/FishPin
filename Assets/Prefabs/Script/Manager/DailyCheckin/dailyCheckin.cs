using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dailyCheckin : MonoBehaviour
{
    public static dailyCheckin Instance;

    [SerializeField] GameObject general;
    [SerializeField] GameObject day7;
    [SerializeField] double nextLoginCheckDelay;
    [SerializeField] double loginDelayLost;
    [SerializeField] float checkTimeDelay = 1f;
    bool streakCheck = true;

    DateTime currentDatetime;

    private void Awake()
    {
        Instance = (dailyCheckin)this;
    }
    private void OnEnable()
    {
        if (WorldTimeAPI.Instance.IsTimeLodaed)
        {
            currentDatetime = WorldTimeAPI.Instance.GetCurrentDateTime();
        }
        else
        {
            currentDatetime = DateTime.Now;
        }
        for (int i = 0; i < general.transform.childCount; i++)
        {
            /*general.transform.GetChild(i).GetComponent<>*/
        }
    }

    IEnumerator logindaily()
    {
        while (true)
        {
            DateTime date = DateTime.Parse(PlayerPrefs.GetString("loginday", currentDatetime.ToString()));
            double elapsedDay = (currentDatetime - date).TotalSeconds;
            if (elapsedDay > loginDelayLost)
            {
                //reset streak
                GameData.resetLoginDay();
                PlayerPrefs.SetString("loginday", currentDatetime.ToString());
            }
            else
            {
                if (elapsedDay > nextLoginCheckDelay)
                {
                    //update streak
                    GameData.increasLoginDay();
                    PlayerPrefs.SetString("loginday", currentDatetime.ToString());
                }
            }
            yield return new WaitForSeconds(checkTimeDelay);
        }
    }

    private void Update()
    {
        if (WorldTimeAPI.Instance.IsTimeLodaed)
        {
            currentDatetime = WorldTimeAPI.Instance.GetCurrentDateTime();
        }
        else
        {
            currentDatetime = DateTime.Now;
        }
    }
}
