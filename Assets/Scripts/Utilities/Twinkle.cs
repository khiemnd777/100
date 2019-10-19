using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Twinkle : MonoBehaviour
{
    [SerializeField]
    float _magnitude;
    [SerializeField]
    float _beatInSeconds;
    [SerializeField]
    Transform _wrapperDisplay;
    [SerializeField]
    SpriteRenderer _renderer;

    void Awake ()
    {
        _renderer.enabled = false;
    }

    public void Play ()
    {
        _renderer.enabled = true;
        StartCoroutine ("Twinkling");
    }

    public void Stop ()
    {
        _renderer.enabled = false;
        StopCoroutine ("Twinkling");
    }

    IEnumerator Twinkling ()
    {
        while (gameObject)
        {
            _wrapperDisplay.localScale = Vector3.one * ComputeTwinkleMagnitude (_magnitude);
            yield return new WaitForSeconds (_beatInSeconds);
        }
    }

    float ComputeTwinkleMagnitude (float magnitude)
    {
        var mag = Random.Range (-magnitude, magnitude);
        return mag == 0 || mag <= magnitude / 3f ? magnitude / 1.25f : mag;
    }
}
