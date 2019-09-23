using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheGoingDown : MonoBehaviour
{
    public float initSpeed;
    public float acceleration;
    public float accelerationWaitedTime = .5f;
    [SerializeField]
    bool _useFx;
    [SerializeField]
    ParticleSystem _fxPrefab;
    [SerializeField]
    float _fxWaitedInitTime;
    [SerializeField]
    ParticleSystem _fxAtCollision;
    [SerializeField]
    AudioSource _soundFxAtCollision;
    CameraShake _shakeCamera;
    float _speed;

    void Awake ()
    {
        _shakeCamera = FindObjectOfType<CameraShake> ();
    }

    void Start ()
    {
        _speed = initSpeed;
        StartCoroutine (WaitToAcceleration ());
        if (_useFx)
        {
            StartCoroutine (InitFx ());
        }
    }

    void Update ()
    {
        transform.Translate (Vector3.down * Time.deltaTime * _speed);
    }

    IEnumerator WaitToAcceleration ()
    {
        if (accelerationWaitedTime <= 0) yield break;
        yield return new WaitForSeconds (accelerationWaitedTime);
        _speed = acceleration;
    }

    IEnumerator InitFx ()
    {
        while (gameObject)
        {
            Instantiate (_fxPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds (_fxWaitedInitTime);
        }
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<TheShepherd> ().Hit ();
            _shakeCamera.Shake (.175f, .065f);
            InstantiateEffectAtCollision ();
            InstantiateSoundEffectAtCollision ();
            Destroy (gameObject);
        }
        else if (other.tag == "The House")
        {
            StartCoroutine (Disappear ());
        }
        else if (other.tag == "The Word")
        {
            other.GetComponent<ThePrayerWord> ().SelfDestruct ();
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

    IEnumerator Disappear ()
    {
        var t = 0f;
        var originScale = transform.localScale;
        while (t <= 1f)
        {
            t += Time.deltaTime * 20f;
            transform.localScale = Vector3.Lerp (originScale, Vector3.zero, t);
            yield return null;
        }
        Destroy (gameObject);
    }
}
