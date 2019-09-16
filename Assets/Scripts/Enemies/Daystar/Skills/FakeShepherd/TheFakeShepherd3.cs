using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheFakeShepherd3 : MonoBehaviour
{
    public float initSpeed;
    [SerializeField]
    bool _useFx;
    [SerializeField]
    ParticleSystem _fxPrefab;
    [SerializeField]
    float _fxWaitedInitTime;
    float _speed;

    void Awake ()
    {
        transform.localScale = Vector3.zero;
    }

    void Start ()
    {
        _speed = initSpeed;
        StartCoroutine (Script ());
        if (_useFx)
        {
            StartCoroutine (InitFx ());
        }
    }

    IEnumerator Script ()
    {
        yield return StartCoroutine (Appear ());
        yield return StartCoroutine (Prepare ());
        transform.localScale = new Vector3 (.7f, 1.9f, 1);
        while (gameObject)
        {
            transform.Translate (Vector3.down * Time.deltaTime * _speed);
            yield return null;
        }
    }

    IEnumerator Appear ()
    {
        var t = 0f;
        while (t <= 1f)
        {
            transform.localScale = Vector3.Lerp (Vector3.zero, Vector3.one, t);
            t += Time.deltaTime / .4f;
            yield return null;
        }
    }

    IEnumerator Prepare ()
    {
        var t = 0f;
        var original = transform.position;
        var expect = original + new Vector3 (0, .4f, 0);
        var expectedScale = new Vector3 (1, .25f, 1);
        while (t <= 1f)
        {
            transform.position = Vector3.Lerp (original, expect, t);
            transform.localScale = Vector3.Lerp (Vector3.one, expectedScale, t);
            t += Time.deltaTime / .124f;
            yield return null;
        }
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
