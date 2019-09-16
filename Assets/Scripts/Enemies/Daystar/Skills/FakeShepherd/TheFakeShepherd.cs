using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheFakeShepherd : MonoBehaviour
{
    public float initSpeed;
    public float acceleration;
    public float accelerationWaitedTime = .5f;
    [SerializeField]
    ParticleSystem _fxPrefab;
    [SerializeField]
    float _fxWaitedInitTime;
    float _speed;

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
