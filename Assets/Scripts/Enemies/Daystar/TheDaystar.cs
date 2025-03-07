﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheDaystar : MonoBehaviour
{
    [SerializeField]
    float _stepUp;
    [SerializeField]
    List<TheDayStarGetHit> _meshHits;
    [SerializeField]
    ParticleSystem _hitFx;
    [SerializeField]
    SpriteRenderer _display;
    [SerializeField]
    Sprite[] _appearances;
    [SerializeField]
    TheDaystarTransform _theDaystarTransformPrefab;
    [SerializeField]
    AudioSource _changeAppearanceSoundFxAtCollision;
    ObjectShake _shake;
    HealthPoint _hp;
    TheDaystarSkillController _skillController;
    Settings _settings;
    bool _isHit;
    int _currentAppearanceState;
    Sprite _originalDisplay;
    bool _permanent;

    void Awake ()
    {
        _originalDisplay = _display.sprite;
        _settings = FindObjectOfType<Settings> ();
        _shake = GetComponent<ObjectShake> ();
        _meshHits.ForEach (x => x.onHit += OnHit);
        _hp = GetComponent<HealthPoint> ();
        _skillController = GetComponent<TheDaystarSkillController> ();
        _currentAppearanceState = _appearances.Length;
    }

    void Start ()
    {
        ExecuteSkill ();
    }

    void OnHit (Collider other)
    {
        if ("The Word".Equals (other.tag))
        {
            var theWord = other.GetComponent<ThePrayerWord> ();
            // MoveUp (_stepUp);
            _hp.TakeDamage (theWord.damage);
            _shake.Shake ();
            ChangeAppearance ();
            InstantiateHitFx (theWord.transform.position);
            theWord.SelfDestruct ();
        }
        if ("The Traitor Bullet".Equals (other.tag))
        {
            var theTraitorBullet = other.GetComponent<TheTraitorBullet> ();
            _hp.TakeDamage (theTraitorBullet.damage);
            _shake.Shake ();
            theTraitorBullet.SelfDestruct ();
        }
    }

    void ChangeAppearance ()
    {
        if (_hp.hp <= 0)
        {
            ExecuteTransform ();
            return;
        }
        var normalizedHp = _hp.normalize;
        var ratio = 1f / _appearances.Length * _currentAppearanceState;
        if (normalizedHp <= ratio)
        {
            InstantiateDeathSoundEffectAtCollision ();
            _display.sprite = _appearances[_appearances.Length - _currentAppearanceState];
            --_currentAppearanceState;
        }
    }

    void InstantiateDeathSoundEffectAtCollision ()
    {
        if (!_changeAppearanceSoundFxAtCollision) return;
        Instantiate (_changeAppearanceSoundFxAtCollision, transform.position, Quaternion.identity);
    }

    public void Hit (float damage)
    {
        _hp.TakeDamage (damage / 4f);
        ChangeAppearance ();
        _shake.Shake ();
    }

    public void RestoreHp (float restoredHp = 0)
    {
        _currentAppearanceState = _appearances.Length;
        _hp.SetMaxHp (restoredHp == 0 ? _hp.maxHp : restoredHp);
        _hp.SetHp (restoredHp == 0 ? _hp.maxHp : restoredHp);
        _display.sprite = _originalDisplay;
        _permanent = true;
    }

    public void ExecuteSkill ()
    {
        _skillController.Play ();
    }

    public float GetMaxHp ()
    {
        return _hp.maxHp;
    }

    public float GetHp ()
    {
        return _hp.hp;
    }

    public float GetNormalizeHp ()
    {
        return _hp.normalize;
    }

    void ExecuteTransform ()
    {
        _settings.activedSlowUpMove = false;
        _settings.ActiveSlowUpMove (false);
        var ins = Instantiate<TheDaystarTransform> (_theDaystarTransformPrefab, transform.position, Quaternion.identity);
        ins.permanent = _permanent;
        gameObject.SetActive (false);
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
        { }
        else if ("Daystar Win Point".Equals (other.tag)) { }
    }
}
