using Firebase;
using Firebase.Analytics;
using UnityEngine;

public class FirebaseInit : Singleton<FirebaseInit>
{
    // Start is called before the first frame update
    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
        });

    }

    public void EventAds()
    {
        
    }

    public void OpenGame()
    {
        FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventAppOpen);
    }

    public void OnCloseGame()
    {
        FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLevelEnd);
    }

    public void WatchAds()
    {
        FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventAdImpression);
    }
}
