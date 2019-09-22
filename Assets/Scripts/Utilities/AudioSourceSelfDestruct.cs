using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (AudioSource))]
public class AudioSourceSelfDestruct : MonoBehaviour
{
    AudioSource _audioSource;

    void Awake ()
    {
        _audioSource = GetComponent<AudioSource> ();
    }

    void Start ()
    {
        Destroy (gameObject, _audioSource.clip.length);
    }
}
