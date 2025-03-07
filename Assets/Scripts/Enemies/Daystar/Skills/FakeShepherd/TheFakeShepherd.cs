﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheFakeShepherd : MonoBehaviour
{
    [System.NonSerialized]
    public TheTraitor theTraitor;
    public float initSpeed;
    public float acceleration;
    public float accelerationWaitedTime = .5f;
    [SerializeField]
    ParticleSystem _fxPrefab;
    [SerializeField]
    float _fxWaitedInitTime;
    [SerializeField]
    ParticleSystem _fxAtCollision;
    [SerializeField]
    AudioSource _soundFxAtCollision;
    CameraShake _shakeCamera;
    TheDaystar _theDaystar;
    float _speed;

    void Awake ()
    {
        _shakeCamera = FindObjectOfType<CameraShake> ();
        theTraitor = GetComponent<TheTraitor> ();
        _theDaystar = FindObjectOfType<TheDaystar> ();
    }

    void Start ()
    {
        _speed = initSpeed;
        StartCoroutine (WaitToAcceleration ());
        // StartCoroutine (InitFx ());
    }

    void Update ()
    {
        transform.Translate (Vector3.down * Time.deltaTime * _speed);
    }

    IEnumerator WaitToAcceleration ()
    {
        yield return new WaitForSeconds (accelerationWaitedTime);
        _speed = acceleration;
        transform.localScale = new Vector3 (.7f, 1.9f, 1);
    }

    IEnumerator InitFx ()
    {
        while (gameObject)
        {
            Instantiate (_fxPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds (_fxWaitedInitTime);
        }
    }

    void InstantiateEffectAtCollision ()
    {
        if (!_fxAtCollision) return;
        Instantiate (_fxAtCollision, transform.position, Quaternion.identity);
    }

    void InstantiateSoundEffectAtCollision ()
    {
        if (!_soundFxAtCollision) return;
        Instantiate (_soundFxAtCollision, transform.position, Quaternion.identity);
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.tag == "Player")
        {
            var normalizedHp = _theDaystar.GetNormalizeHp ();
            var damage = normalizedHp <= (1f / 7f) ?
                (theTraitor.isTraitor ? 7 : 3) :
                (theTraitor.isTraitor ? 5 : 1);
            other.GetComponent<TheShepherd> ().Hit (damage);
            _shakeCamera.Shake (.175f, .065f);
            InstantiateEffectAtCollision ();
            InstantiateSoundEffectAtCollision ();
            if (theTraitor.isTraitor)
            {
                theTraitor.InstantiateBullet (300f);
            }
            Destroy (gameObject);
        }
        else if (other.tag == "The House")
        {
            Destroy (gameObject);
        }
        else if (other.tag == "The Word")
        {
            other.GetComponent<ThePrayerWord> ().SelfDestruct ();
        }
    }
}
