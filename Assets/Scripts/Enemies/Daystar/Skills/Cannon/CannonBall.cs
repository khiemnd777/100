using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
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
