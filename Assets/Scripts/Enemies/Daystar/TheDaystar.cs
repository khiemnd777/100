using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheDaystar : MonoBehaviour
{
    HealthPoint _hp;

    void Awake ()
    {
        _hp = GetComponent<HealthPoint> ();
        _hp.OnDeath += OnDeath;
    }

    void OnDeath ()
    {
        Debug.Log (name + " is dead.");
    }
}
