using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earthquake : MonoBehaviour
{
    public float duration;
    public float magnitude;
    CameraShake _cameraShake;

    void Awake ()
    {
        _cameraShake = FindObjectOfType<CameraShake> ();
    }

    void Start ()
    {
        if (_cameraShake)
        {
            StartCoroutine (Shake ());
        }
    }

    IEnumerator Shake ()
    {
        while (gameObject)
        {
            _cameraShake.Shake (duration, magnitude);
            yield return new WaitForSeconds (duration);
        }
    }
}
