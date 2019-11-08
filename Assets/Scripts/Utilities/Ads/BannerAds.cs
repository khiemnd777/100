using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

public class BannerAds : MonoBehaviour
{
#if UNITY_IOS
    private string gameId = "3341425";
#elif UNITY_ANDROID
    private string gameId = "3341424";
#endif
    public string placementId = "banner";
    public bool testMode = true;

    void Start ()
    {
        Advertisement.Initialize (gameId, testMode);
        Advertisement.Banner.SetPosition (BannerPosition.BOTTOM_CENTER);
        StartCoroutine (ShowBannerWhenReady ());
    }

    IEnumerator ShowBannerWhenReady ()
    {
        while (!Advertisement.IsReady (placementId))
        {
            yield return new WaitForSeconds (0.5f);
        }
        Advertisement.Banner.Show (placementId);
    }
}
