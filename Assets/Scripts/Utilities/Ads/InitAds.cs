using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class InitAds : MonoBehaviour
{
#if UNITY_IOS
    private string gameId = "3341425";
#elif UNITY_ANDROID
    private string gameId = "3341424";
#endif
    public bool testMode = true;
    void Start ()
    {
        Advertisement.Initialize (gameId, testMode);
    }
}
