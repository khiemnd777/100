using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheDaystar : MonoBehaviour
{
    HealthPoint _hp;
    [SerializeField]
    List<TheDayStarGetHit> _meshHits;
    bool _isHit;
    void Awake ()
    {
        _hp = GetComponent<HealthPoint> ();
        _hp.OnDeath += OnDeath;
        _meshHits.ForEach (x => x.onHit += OnHit);
    }

    void OnHit (Collider other)
    {
        if (!"The Word".Equals (other.tag)) return;
        if (_isHit) return;
        _isHit = true;
        var theWord = other.GetComponent<ThePrayerWord> ();
        _hp.TakeDamage (theWord.damage);
        theWord.SelfDestruct ();
    }

    void LateUpdate ()
    {
        _isHit = false;
    }

    void OnDeath ()
    {
        Debug.Log (name + " is dead.");
    }

    void OnTriggerEnter (Collider other)
    {
        if ("Daystar Death Point".Equals (other.tag))
        {
            Debug.Log (0);
        }
        else if ("Daystar Win Point".Equals (other.tag))
        {
            Debug.Log (1);
        }
    }
}
