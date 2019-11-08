using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

public class VideoAds : MonoBehaviour, IUnityAdsListener
{
#if UNITY_IOS
    private string gameId = "3341425";
#elif UNITY_ANDROID
    private string gameId = "3341424";
#endif
    public string placementId = "video";
    public bool testMode = true;
    public event System.Action<ShowResult> onAdsDidFinish;

    void Start ()
    {
        Advertisement.AddListener (this);
        Advertisement.Initialize (gameId, testMode);
    }

    public void Show ()
    {
        if (!IsReady ()) return;
        Advertisement.Show (placementId);
    }

    public bool IsReady ()
    {
        return Advertisement.IsReady (placementId);
    }

    public void OnUnityAdsReady (string placementId) { }

    public void OnUnityAdsDidError (string message)
    {

    }

    public void OnUnityAdsDidStart (string placementId) { }

    public void OnUnityAdsDidFinish (string placementId, ShowResult showResult)
    {
        if (onAdsDidFinish != null)
        {
            onAdsDidFinish (showResult);
        }
    }
}
