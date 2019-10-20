using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    [System.NonSerialized]
    public TheTraitor theTraitor;
    [System.NonSerialized]
    public float speed = 5f;
    [System.NonSerialized]
    public Vector3 forward;
    [SerializeField]
    ParticleSystem _fxPrefab;
    [SerializeField]
    float _fxWaitedInitTime;
    [SerializeField]
    Transform _display;
    [SerializeField]
    Transform _fxAtCollision;
    [SerializeField]
    AudioSource _soundFxAtCollision;
    CameraShake _shakeCamera;

    void Awake ()
    {
        _shakeCamera = FindObjectOfType<CameraShake> ();
        theTraitor = GetComponent<TheTraitor> ();
    }

    void Start ()
    {
        StartCoroutine (InitFx ());
        Destroy (gameObject, 3f);
    }

    void Update ()
    {
        // translate
        transform.Translate (forward * Time.deltaTime * speed);
        // rotate itself.
        _display.transform.RotateAround (_display.transform.position, Vector3.forward, Time.deltaTime * 500f);
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
            other.GetComponent<TheShepherd> ().Hit (theTraitor.isTraitor ? 5 : 1);
            _shakeCamera.Shake (.175f, .065f);
            InstantiateEffectAtCollision ();
            InstantiateSoundEffectAtCollision ();
            if (theTraitor.isTraitor)
            {
                theTraitor.InstantiateBullet (100f);
            }
            Destroy (gameObject);
        }
        else if (other.tag == "The House")
        {
            InstantiateEffectAtCollision ();
            InstantiateSoundEffectAtCollision ();
            Destroy (gameObject);
        }
    }
}
