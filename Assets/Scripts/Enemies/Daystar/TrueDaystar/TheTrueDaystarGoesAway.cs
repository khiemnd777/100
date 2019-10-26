using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheTrueDaystarGoesAway : MonoBehaviour
{
    [SerializeField]
    float _deltaForce;
    Rigidbody _rigid;
    bool _stop;

    void Awake ()
    {
        _rigid = GetComponent<Rigidbody> ();
    }

    void Update ()
    {
        if (_stop) return;
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

    public void Stop ()
    {
        _stop = true;
        _rigid.velocity = Vector3.zero;
    }

    public void KeepGoingDown ()
    {
        _stop = false;
    }
}
