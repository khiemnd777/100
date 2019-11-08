using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectShake : MonoBehaviour
{
    public Transform shakedObject;
    public float duration;
    public float magnitude;
    Vector3 _originalLocalPosition;
    Settings _settings;

    void Awake ()
    {
        _settings = FindObjectOfType<Settings> ();
    }

    void Start ()
    {
        _originalLocalPosition = shakedObject.localPosition;
    }

    public void Shake (bool vibrate = false)
    {
        Shake (duration, magnitude, vibrate);
    }

    public void Shake (float duration, float magnitude, bool vibrate = false)
    {
        StartCoroutine (Shaking (duration, magnitude, vibrate));
    }

    public IEnumerator Shaking (float duration, float magnitude, bool vibrate = false)
    {
        var elapsed = 0f;
        while (elapsed <= 1f)
        {
            elapsed += Time.unscaledDeltaTime / duration;
            shakedObject.localPosition = _originalLocalPosition + Random.insideUnitSphere * magnitude;
            yield return null;
        }
        shakedObject.localPosition = _originalLocalPosition;
        if (vibrate)
        {
            _settings.Vibrate ();
        }
    }
}
