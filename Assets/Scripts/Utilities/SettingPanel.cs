using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : MonoBehaviour
{
    [SerializeField]
    SettingData _settingData;
    Settings _settings;
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
        RenderVibrationStatus ();
        RenderSlowmotionStatus ();
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
