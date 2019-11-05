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
    public Button back;

    void Awake ()
    {
        _settings = FindObjectOfType<Settings> ();
        vibrationOn.onClick.AddListener (VibrationOff);
        vibrationOff.onClick.AddListener (VibrationOn);
        back.onClick.AddListener (Back);
    }

    void Start ()
    {
        RenderVibrationStatus ();
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

    void Back ()
    {
        _settings.SetInteractableButtons (true);
        _settings.Play ();
        gameObject.SetActive (false);
    }
}
