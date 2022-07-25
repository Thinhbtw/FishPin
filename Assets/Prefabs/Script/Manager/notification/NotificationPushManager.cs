using System.Collections;
using System.Collections.Generic;
using Unity.Notifications.Android;
using UnityEngine;

public class NotificationPushManager : MonoBehaviour
{
    public static NotificationPushManager instance;
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
        notification.Title = "Your Title";
        notification.Text = "Your Text";
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

        //If the script is run and a message is already scheduled, cancel it and re-schedule another message
        if(AndroidNotificationCenter.CheckScheduledNotificationStatus(nofId) == NotificationStatus.Scheduled)
        {
            AndroidNotificationCenter.CancelNotification(nofId);
            AndroidNotificationCenter.SendNotification(notification, channelId);
        }
    }
}
