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
    ParticleSystem _hitFx;
    [SerializeField]
    SpriteRenderer _display;
    [SerializeField]
    Sprite[] _appearances;
    ObjectShake _shake;
    HealthPoint _hp;
    bool _isHit;
    int _currentAppearanceState;

    void Awake ()
    {
        _shake = GetComponent<ObjectShake> ();
        _meshHits.ForEach (x => x.onHit += OnHit);
        _hp = GetComponent<HealthPoint> ();
        _currentAppearanceState = _appearances.Length;
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
        // MoveUp (_stepUp);
        _hp.TakeDamage (theWord.damage);
        _shake.Shake ();
        ChangeAppearance ();
        InstantiateHitFx (theWord.transform.position);
        theWord.SelfDestruct ();
    }

    void ChangeAppearance ()
    {
        if (_hp.hp <= 0) return;
        var normalizedHp = _hp.normalize;
        var ratio = 1f / _appearances.Length * _currentAppearanceState;
        if (normalizedHp <= ratio)
        {
            _display.sprite = _appearances[_appearances.Length - _currentAppearanceState];
            --_currentAppearanceState;
        }
    }

    void MoveUp (float stepUp)
    {
        transform.Translate (Vector3.up * _stepUp * Time.deltaTime);
    }

    void InstantiateHitFx (Vector3 hitPosition)
    {
        Instantiate (_hitFx, hitPosition, Quaternion.identity);
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
