using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Button playAgainButton;
    public string defaultScene;
    VideoAds _videoAds;
    [SerializeField]
    SettingData _settingData;

    void Awake ()
    {
        _videoAds = FindObjectOfType<VideoAds> ();
        if (_videoAds)
        {
            _videoAds.onAdsDidFinish += NavigateScene;
        }
        playAgainButton.onClick.AddListener (PlayAgain);
    }

    void PlayAgain ()
    {
        playAgainButton.interactable = false;
        if (string.IsNullOrEmpty (defaultScene)) return;
        if (_settingData.CanShowVideoAds ())
        {
            if (_videoAds && _videoAds.IsReady ())
            {
                _videoAds.Show ();
                return;
            }
        }
        LoadScene ();
    }

    void NavigateScene (ShowResult showResult)
    {
        LoadScene ();
    }

    void LoadScene ()
    {
        SceneManager.LoadScene (string.Format ("Scenes/{0}", defaultScene));
        Time.timeScale = 1f;
        Destroy (gameObject);
    }
}
