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
    ParticleSystem _hitFx;
    [SerializeField]
    SpriteRenderer _display;
    [SerializeField]
    Sprite[] _appearances;
    [SerializeField]
    TheDaystarTransform _theDaystarTransformPrefab;
    ObjectShake _shake;
    HealthPoint _hp;
    TheDaystarSkillController _skillController;
    bool _isHit;
    int _currentAppearanceState;
    Sprite _originalDisplay;
    bool _permanent;

    void Awake ()
    {
        _originalDisplay = _display.sprite;
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
            _display.sprite = _appearances[_appearances.Length - _currentAppearanceState];
            --_currentAppearanceState;
        }
    }

    public void RestoreHp ()
    {
        _currentAppearanceState = _appearances.Length;
        _hp.Recovery (_hp.maxHp);
        _display.sprite = _originalDisplay;
        _permanent = true;
    }

    public void ExecuteSkill ()
    {
        _skillController.Play ();
    }

    void ExecuteTransform ()
    {
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
        {
            Debug.Log (0);
        }
        else if ("Daystar Win Point".Equals (other.tag)) { }
    }
}
