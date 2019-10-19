using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheTraitorBulletImpact : MonoBehaviour
{
    [SerializeField]
    Transform _wrapper;
    [SerializeField]
    float _scaleTime;

    void Awake ()
    {
        _wrapper.localScale = Vector3.zero;
    }

    void Start ()
    {
        StartCoroutine (ScaleOut ());
    }

    IEnumerator ScaleOut ()
    {
        var t = 0f;
        while (t <= 1f)
        {
            t += Time.deltaTime / _scaleTime;
            _wrapper.localScale = Vector3.Lerp (Vector3.zero, Vector3.one, t);
            yield return null;
        }
        Destroy (gameObject);
    }
}
