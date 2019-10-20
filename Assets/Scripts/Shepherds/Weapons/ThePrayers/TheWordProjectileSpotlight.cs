using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheWordProjectileSpotlight : MonoBehaviour
{
    [SerializeField]
    Transform _wrapper;

    void Start ()
    {
        StartCoroutine (ScaleIn ());
    }

    IEnumerator ScaleIn ()
    {
        var t = 0f;
        while (t <= 1f)
        {
            t += Time.deltaTime / .225f;
            _wrapper.localScale = Vector3.Lerp (Vector3.one, Vector3.zero, t);
            yield return null;
        }
        Destroy (gameObject);
    }
}
