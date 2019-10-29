using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class HideBannerAds : MonoBehaviour
{
    void Start ()
    {
        Advertisement.Banner.Hide ();
    }
}
