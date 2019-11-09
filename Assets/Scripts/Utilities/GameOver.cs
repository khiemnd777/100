using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Button playAgainButton;
    public string defaultScene;
    [System.NonSerialized]
    public VideoAds videoAds;
    [SerializeField]
    SettingData _settingData;

    void Awake ()
    {
        playAgainButton.onClick.AddListener (PlayAgain);
    }

    void Start ()
    {
        if (videoAds)
        {
            videoAds.onAdsDidFinish = NavigateScene;
        }
    }

    void PlayAgain ()
    {
        playAgainButton.interactable = false;
        if (string.IsNullOrEmpty (defaultScene)) return;
        if (_settingData.CanShowVideoAds ())
        {
            if (videoAds && videoAds.IsReady ())
            {
                videoAds.Show ();
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
