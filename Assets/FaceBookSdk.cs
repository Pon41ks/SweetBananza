using UnityEngine;
using Facebook.Unity;

public class FaceBookSdk : MonoBehaviour
{
    void Awake()
    {
        if (!FB.IsInitialized)
        {
            FB.Init(InitCallback, OnHideUnity);
        }
        else
        {
            Debug.Log("Facebook SDK already initialized.");
            FetchAppLinkData();
        }
        DontDestroyOnLoad(gameObject);
    }

    private void InitCallback()
    {
        if (FB.IsInitialized)
        {
            FB.ActivateApp();
            Debug.Log("Facebook SDK initialized successfully.");
        }
        else
        {
            Debug.LogError("Failed to Initialize the Facebook SDK");
        }
    }

    private void OnHideUnity(bool isGameShown)
    {
        Time.timeScale = isGameShown ? 1 : 0;
    }

    private void FetchAppLinkData()
    {
        FB.Mobile.FetchDeferredAppLinkData(OnAppLinkDataReceived);
    }

    private void OnAppLinkDataReceived(IAppLinkResult result)
    {
        if (!string.IsNullOrEmpty(result.Url))
        {
            Debug.Log("App link data received: " + result.Url);
            
        }
        else
        {
            Debug.Log("No app link data received");
        }
    }

    private void OnDestroy()
    {
        // Очистка объекта FaceBookSdk
    }

    public static void LogAppInstall()
    {
        if (FB.IsInitialized)
        {
           
            FB.LogAppEvent(AppEventName.ActivatedApp);
            Debug.Log("Logged app install event.");
        }
        else
        {
            Debug.LogError("Facebook SDK is not initialized.");
        }
    }
}
