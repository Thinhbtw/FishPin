using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dropdownNotif : MonoBehaviour
{
    [SerializeField] List<GameObject> notifications;
    [SerializeField] GameObject dropdownNotification;
    void Update()
    {
        foreach(GameObject notification in notifications)
        {
            if (notification.activeSelf)
            {
                dropdownNotification.SetActive(true);
                return;
            }
        }
        dropdownNotification.SetActive(false);
    }
}
