using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaylistManager : MonoBehaviour
{
    [Range (0f, 1f)]
    public float volume;
    [SerializeField]
    AudioSource[] _sources;
    AudioSource _currentSource;

    void Start ()
    {
        if (_sources.Length == 0) return;
        StartCoroutine (Play ());
    }

    void Update ()
    {
        if (_currentSource)
        {
            if (Time.timeScale == 0)
            {
                _currentSource.Pause ();
            }
            else
            {
                _currentSource.UnPause ();
            }
        }
        AdjustVolume ();
    }

    void AdjustVolume ()
    {
        foreach (var source in _sources)
        {
            source.volume = volume;
        }
    }

    IEnumerator Play ()
    {
        var inx = 0;
        while (gameObject)
        {
            if (inx >= _sources.Length)
            {
                inx = 0;
            }
            _currentSource = _sources[inx];
            _currentSource.Play ();
            ++inx;
            yield return new WaitForSeconds (_currentSource.clip.length + .5f);
        }
    }
}
