using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemSelfDestruct : MonoBehaviour
{
    ParticleSystem _ps;

    void Awake ()
    {
        _ps = GetComponent<ParticleSystem> ();
    }

    // Update is called once per frame
    void Update ()
    {
        if (!_ps) return;
        if (_ps.IsAlive ()) return;
        Destroy (gameObject);
    }
}
