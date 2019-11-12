using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAtLength : MonoBehaviour
{
    public float length;
    PlaylistManager _manager;

    void Awake ()
    {
        _manager = GetComponent<PlaylistManager> ();
    }

    void Start ()
    {
        StartCoroutine (Play ());
    }

    IEnumerator Play ()
    {
        yield return new WaitForSecondsRealtime (length);
        _manager.Play ();
    }
}
