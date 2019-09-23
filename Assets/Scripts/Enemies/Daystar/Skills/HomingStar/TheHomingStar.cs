using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheHomingStar : MonoBehaviour
{
    [System.NonSerialized]
    public float speed = 5f;
    [System.NonSerialized]
    public float maxDegreesDeltaRotation = 100f;
    [System.NonSerialized]
    public float acceleration = 7f;
    [System.NonSerialized]
    public float maxDegreesDeltaRotationAcceleration = 100f;
    [System.NonSerialized]
    public float accelerationWaitedTime = .5f;
    [SerializeField]
    ParticleSystem _fxPrefab;
    [SerializeField]
    float _fxWaitedInitTime;
    TheShepherd _theShepherd;
    [SerializeField]
    Transform _display;
    [SerializeField]
    Transform _fxAtCollision;
    [SerializeField]
    AudioSource _soundFxAtCollision;
    int _rotationDirection;
    int[] _indicatedRotationDirections = {-1, 1 };
    CameraShake _shakeCamera;

    void Awake ()
    {
        _theShepherd = FindObjectOfType<TheShepherd> ();
        _rotationDirection = _indicatedRotationDirections[Random.Range (0, _indicatedRotationDirections.Length)];
        _shakeCamera = FindObjectOfType<CameraShake> ();
    }

    void Start ()
    {
        var forward = _theShepherd.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation (Vector3.forward, forward);
        transform.rotation = Quaternion.Euler (0f, 0f, 90f * _rotationDirection);
        StartCoroutine (WaitToAcceleration ());
        StartCoroutine (InitFx ());
        Destroy (gameObject, 3f);
    }

    void Update ()
    {
        // translate & rotate towards target.
        var forward = _theShepherd.transform.position - transform.position;
        forward.Normalize ();
        var towardRotation = Quaternion.LookRotation (Vector3.forward, forward);
        transform.rotation = Quaternion.RotateTowards (transform.rotation, towardRotation, Time.deltaTime * maxDegreesDeltaRotation);
        transform.Translate (Vector3.up * Time.deltaTime * speed);
        // rotate itself.
        _display.transform.RotateAround (_display.transform.position, Vector3.forward * _rotationDirection, Time.deltaTime * 700f);
    }

    IEnumerator WaitToAcceleration ()
    {
        yield return new WaitForSeconds (accelerationWaitedTime);
        speed = acceleration;
        maxDegreesDeltaRotation = maxDegreesDeltaRotationAcceleration;
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
        if ("Player".Equals (other.tag))
        {
            other.GetComponent<TheShepherd> ().Hit ();
            _shakeCamera.Shake (.175f, .065f);
            InstantiateEffectAtCollision ();
            InstantiateSoundEffectAtCollision ();
            Destroy (gameObject);
        }
        else if ("The House".Equals (other.tag))
        {
            InstantiateEffectAtCollision ();
            InstantiateSoundEffectAtCollision ();
            Destroy (gameObject);
        }
        else if ("The Word".Equals (other.tag))
        {
            other.GetComponent<ThePrayerWord> ().SelfDestruct ();
        }
    }
}
