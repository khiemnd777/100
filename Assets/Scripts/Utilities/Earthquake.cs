using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earthquake : MonoBehaviour
{
    public float duration;
    public float magnitude;
    CameraShake _cameraShake;
    bool _stop;

    void Awake ()
    {
        _cameraShake = FindObjectOfType<CameraShake> ();
        _stop = false;
    }

    public void StartEarthquake ()
    {
        _stop = false;
        if (_cameraShake)
        {
            StartCoroutine ("Shake");
        }
    }

    IEnumerator Shake ()
    {
        while (!_stop)
        {
            _cameraShake.Shake (duration, magnitude);
            yield return new WaitForSeconds (duration);
        }
    }

    public void StopEarthquake ()
    {
        _stop = true;
    }
}
