using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class SettingPanel : MonoBehaviour
{
    [SerializeField]
    SettingData _settingData;
    Settings _settings;
    [SerializeField]
    RewardedAds _rewardedAds;
    public Button vibrationOn;
    public Button vibrationOff;
    public Button slowmotionOn;
    public Button slowmotionOff;
    public Button back;

    void Awake ()
    {
        _settings = FindObjectOfType<Settings> ();
        vibrationOn.onClick.AddListener (VibrationOff);
        vibrationOff.onClick.AddListener (VibrationOn);
        slowmotionOn.onClick.AddListener (SlowmotionOff);
        slowmotionOff.onClick.AddListener (SlowmotionOn);
        back.onClick.AddListener (Back);
    }

    void Start ()
    {
        SlowmotionInteractable (false);
        RenderVibrationStatus ();
        RenderSlowmotionStatus ();
        if (_rewardedAds)
        {
            _rewardedAds.onAdsDidFinish = OnRewardedAdsDidFinish;
            StartCoroutine (InteractSlowmotion ());
        }
    }

    IEnumerator InteractSlowmotion ()
    {
        while (!Advertisement.IsReady (_rewardedAds.placementId))
        // while (!_rewardedAds.isReady)
        {
            yield return new WaitForSecondsRealtime (.5f);
        }
        SlowmotionInteractable (true);
    }

    void SlowmotionInteractable (bool interactable)
    {
        slowmotionOn.interactable = interactable;
        slowmotionOff.interactable = interactable;
    }

    void OnRewardedAdsDidFinish (ShowResult showResult)
    {
        TurnSlowmotionOn ();
    }

    void RenderVibrationStatus ()
    {
        if (_settingData.vibrated)
        {
            vibrationOn.gameObject.SetActive (true);
            vibrationOff.gameObject.SetActive (false);
        }
        else
        {
            vibrationOn.gameObject.SetActive (false);
            vibrationOff.gameObject.SetActive (true);
        }
    }

    void RenderSlowmotionStatus ()
    {
        if (_settingData.slowmotion)
        {
            slowmotionOn.gameObject.SetActive (true);
            slowmotionOff.gameObject.SetActive (false);
        }
        else
        {
            slowmotionOn.gameObject.SetActive (false);
            slowmotionOff.gameObject.SetActive (true);
        }
    }

    void VibrationOn ()
    {
        _settingData.vibrated = true;
        RenderVibrationStatus ();
        Handheld.Vibrate ();
    }

    void VibrationOff ()
    {
        _settingData.vibrated = false;
        RenderVibrationStatus ();
    }

    void SlowmotionOn ()
    {
        _rewardedAds.Show ();
    }

    void TurnSlowmotionOn ()
    {
        _settingData.slowmotion = true;
        RenderSlowmotionStatus ();
    }

    void SlowmotionOff ()
    {
        _settingData.slowmotion = false;
        RenderSlowmotionStatus ();
    }

    void Back ()
    {
        _settings.SetInteractableButtons (true);
        _settings.Play ();
        gameObject.SetActive (false);
    }
}
