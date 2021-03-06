using System.Collections;
using System.Collections.Generic;
using Unity.Notifications.Android;
using UnityEngine;

public class NotificationPushManager : MonoBehaviour
{
    #region Singleton class: NotificationPushManager

    public static NotificationPushManager Instance;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    #endregion    

    private static int notificationID;
    /*public static NotificationPushManager instance;
    public static string channelID;
    public static AndroidNotification notif;
    public enum fireTimeType
    {
        seconds,
        minutes,
        hours,
    }

    private void Start()
    {
        instance = (NotificationPushManager)this;

        //Remove all notification that have already been displayed
        AndroidNotificationCenter.CancelAllDisplayedNotifications();

        channelCreating("daily","dailyLogin",Importance.Default,"reminder login");
        notificationCreating("Hop on!", "dont miss everyday gift", NotificationPushManager.fireTimeType.seconds, 5, "daily");
    }

    // Create the android notification channel to send the messages through
    void channelCreating(string channelId,string channelName,Importance channelPriority,string description)
    {
        var channel = new AndroidNotificationChannel()
        {
            Id = channelId,
            Name = channelName,
            Importance = channelPriority,
            Description = description,
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);
    }

    public void notificationCreating(string notificationTitle,string notificationText,fireTimeType type,int Time,string channelId)
    {
        var notification = new AndroidNotification();
        notification.Title = notificationTitle;
        notification.Text = notificationText;
        switch (type)
        {
            case fireTimeType.seconds:
                notification.FireTime = System.DateTime.Now.AddSeconds(Time);
                break;
            case fireTimeType.minutes:
                notification.FireTime = System.DateTime.Now.AddMinutes(Time);
                break;
            case fireTimeType.hours:
                notification.FireTime = System.DateTime.Now.AddHours(Time);
                break;
        }
        //send the notification
        var nofId = AndroidNotificationCenter.SendNotification(notification, channelId);
        if(AndroidNotificationCenter.CheckScheduledNotificationStatus(nofId) == NotificationStatus.Scheduled)
        {
            //reschedule 
            AndroidNotificationCenter.CancelNotification(nofId);
            AndroidNotificationCenter.SendNotification(notification, channelId);
        }
    }
    
    */
    
    private void OnApplicationPause(bool pause)
    {
        if (PlayerPrefs.GetString("SaveSettings").Contains("2"))
        {
            //Remove all notification that have already been displayed
            AndroidNotificationCenter.CancelAllDisplayedNotifications();

            //create channel for the notification
            var channel = new AndroidNotificationChannel()
            {
                Id = "channel_id",
                Name = "Default Channel",
                Importance = Importance.Default,
                Description = "Reminder notifications",
            };
            AndroidNotificationCenter.RegisterNotificationChannel(channel);

            //create notification
            var notification = new AndroidNotification();
            notification.Title = "Hop on!";
            notification.Text = "Dont miss everyday gift";
            notification.FireTime = System.DateTime.Now.AddSeconds(15);

            //sending notification
            notificationID = AndroidNotificationCenter.SendNotification(notification, "channel_id");
            //check if is scheduled
            if (AndroidNotificationCenter.CheckScheduledNotificationStatus(notificationID) == NotificationStatus.Scheduled)
            {
                //reschedule 
                AndroidNotificationCenter.CancelNotification(notificationID);
                AndroidNotificationCenter.SendNotification(notification, "channel_id");
            }

        }
    }
    public void NotificationOnOff(bool stat)
    {
        if (stat)
        {
            /*var notification = new AndroidNotification();
            notification.Title = "Hop on!";
            notification.Text = "Dont miss everyday gift";
            notification.FireTime = System.DateTime.Now.AddSeconds(15);
            if (AndroidNotificationCenter.CheckScheduledNotificationStatus(notificationID) == NotificationStatus.Scheduled)
            {
                //reschedule 
                Debug.Log("scheduled");
                AndroidNotificationCenter.CancelNotification(notificationID);
                AndroidNotificationCenter.SendNotification(notification, "channel_id");
            }
            else if (AndroidNotificationCenter.CheckScheduledNotificationStatus(notificationID) == NotificationStatus.Delivered)
            {
                // Remove the previously shown notification from the status bar.
                Debug.Log("delivered");
                AndroidNotificationCenter.CancelNotification(notificationID);
            }
            else if (AndroidNotificationCenter.CheckScheduledNotificationStatus(notificationID) == NotificationStatus.Unknown)
            {
                Debug.Log("unknown");
                AndroidNotificationCenter.SendNotification(notification, "channel_id");
            }
            OnApplicationPause(true);*/
        }
        else
        {
            AndroidNotificationCenter.CancelAllNotifications();
        }
    }
}
