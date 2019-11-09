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
    public System.Action<ShowResult> onAdsDidFinish;
    public System.Action onAdsReady;
    public bool isReady
    {
        get
        {
            return _isReady;
        }
    }
    bool _isReady;

    void Start ()
    {
        Advertisement.AddListener (this);
        Advertisement.Initialize (gameId, testMode);
    }

    public void Show ()
    {
        Advertisement.Show (placementId);
    }

    public bool IsReady ()
    {
        return Advertisement.IsReady (placementId);
    }

    public void OnUnityAdsReady (string placementId)
    {
        if (placementId != this.placementId) return;
        _isReady = true;
        if (onAdsReady != null)
        {
            onAdsReady ();
        }
    }

    public void OnUnityAdsDidError (string message)
    {

    }

    public void OnUnityAdsDidStart (string placementId) { }

    public void OnUnityAdsDidFinish (string placementId, ShowResult showResult)
    {
        if (placementId != this.placementId) return;
        if (onAdsDidFinish != null)
        {
            onAdsDidFinish (showResult);
        }
    }

    void OnDestroy ()
    {
        onAdsDidFinish = null;
        onAdsReady = null;
    }
}
