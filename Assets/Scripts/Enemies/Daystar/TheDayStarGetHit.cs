using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheDayStarGetHit : MonoBehaviour
{
    public event System.Action<Collider> onHit;
    void OnTriggerEnter (Collider other)
    {
        if (onHit == null) return;
        onHit (other);
    }
}
