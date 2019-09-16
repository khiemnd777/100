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
    int _rotationDirection;
    int[] _indicatedRotationDirections = {-1, 1 };

    void Awake ()
    {
        _theShepherd = FindObjectOfType<TheShepherd> ();
        _rotationDirection = _indicatedRotationDirections[Random.Range (0, _indicatedRotationDirections.Length)];
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

    void OnTriggerEnter (Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy (gameObject);
        }
        else if (other.tag == "The House")
        {
            Destroy (gameObject);
        }
    }
}
