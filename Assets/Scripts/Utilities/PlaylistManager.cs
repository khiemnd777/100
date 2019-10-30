using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaylistManager : MonoBehaviour
{
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
