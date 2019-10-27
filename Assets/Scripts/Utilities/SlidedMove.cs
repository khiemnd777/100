using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidedMove : MonoBehaviour
{
    [SerializeField]
    float _speed;
    Rigidbody _rd;

    void Awake ()
    {
        _rd = GetComponent<Rigidbody> ();
    }

    void FixedUpdate ()
    {
        foreach (var touch in Input.touches)
        {
            _rd.velocity = touch.phase == TouchPhase.Moved ?
                Vector3.right * touch.deltaPosition.x * (Time.deltaTime * 7.5f) :
                Vector3.zero;
        }
    }
}
