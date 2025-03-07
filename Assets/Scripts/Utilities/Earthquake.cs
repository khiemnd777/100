﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earthquake : MonoBehaviour
{
    public float duration;
    public float magnitude;
    CameraShake _cameraShake;
    Settings _settings;
    bool _stop;

    void Awake ()
    {
        _cameraShake = FindObjectOfType<CameraShake> ();
        _settings = FindObjectOfType<Settings> ();
        _stop = false;
    }

    public void StartEarthquake (bool vibrate = false)
    {
        _stop = false;
        if (_cameraShake)
        {
            StartCoroutine (Shake (vibrate));
        }
    }

    IEnumerator Shake (bool vibrate = false)
    {
        while (!_stop)
        {
            _cameraShake.Shake (duration, magnitude);
            if (vibrate)
            {
                _settings.Vibrate ();
            }
            yield return new WaitForSeconds (duration);
        }
    }

    public void StopEarthquake ()
    {
        _stop = true;
    }
}
