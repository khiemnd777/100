using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheTrueDaystarGoesAway : MonoBehaviour
{
    [SerializeField]
    float _deltaForce;
    Gyroscope _gyro;
    Rigidbody _rigid;

    void Awake ()
    {
        _rigid = GetComponent<Rigidbody> ();
    }

    void Start ()
    {
        _gyro = Input.gyro;
        _gyro.enabled = true;
    }

    void Update ()
    {
        var tilt = Input.acceleration;
        if (tilt.y > 0)
        {
            _rigid.AddForce (tilt * _deltaForce);
        }
        else
        {
            _rigid.velocity = Vector3.zero;
        }
    }
}
