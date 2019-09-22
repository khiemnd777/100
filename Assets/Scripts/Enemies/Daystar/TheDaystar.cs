using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheDaystar : MonoBehaviour
{
    [SerializeField]
    float _stepUp;
    [SerializeField]
    List<TheDayStarGetHit> _meshHits;
    [SerializeField]
    Transform _deathPoint;
    [SerializeField]
    ObjectShake _shake;
    bool _isHit;

    void Awake ()
    {
        _shake = GetComponent<ObjectShake> ();
        _meshHits.ForEach (x => x.onHit += OnHit);
    }

    void Update ()
    {
        Debug.DrawLine (transform.position, transform.position + Vector3.right * 5f);
        Debug.DrawLine (_deathPoint.position, _deathPoint.position + Vector3.right * 5f);
    }

    void OnHit (Collider other)
    {
        if (!"The Word".Equals (other.tag)) return;
        var theWord = other.GetComponent<ThePrayerWord> ();
        MoveUp (_stepUp);
        _shake.Shake ();
        theWord.SelfDestruct ();
    }

    void MoveUp (float stepUp)
    {
        transform.Translate (Vector3.up * _stepUp * Time.deltaTime);
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
