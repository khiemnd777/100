using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheTrinityPrayer : MonoBehaviour
{
    public float damage;
    public float speed;
    public float delayBetweenLaunch;
    public bool isTrinity;
    [SerializeField]
    float _lifetime;
    [Header ("The Shield")]
    [SerializeField]
    bool _activatedShield;
    [SerializeField]
    float _shieldDamage;
    [SerializeField]
    float _shieldScaleTime;
    [SerializeField]
    float _shieldScale;
    [SerializeField]
    int _shieldNumber;
    [SerializeField]
    ThePrayerShield _theShieldPrefab;
    [SerializeField]
    Transform _theShieldPoint;
    bool _usingShield;
    [Space]
    [SerializeField]
    Transform _projectilePoint;
    [SerializeField]
    ThePrayerWord _theWordPrefab;
    [SerializeField]
    LayerMask _detectedLayer;
    float _tDelayUp = 1f;

    void Update ()
    {
        LaunchUp ();
    }

    void LaunchUp ()
    {
        if (_tDelayUp <= 1f)
        {
            _tDelayUp += Time.deltaTime / delayBetweenLaunch;
            return;
        }
        _tDelayUp = 0f;
        RaycastHit hit;
        if (Physics.Raycast (_projectilePoint.position, Vector3.up, out hit, float.MaxValue, _detectedLayer))
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer ("Void")) return;
            if (!isTrinity)
            {
                LaunchSide (Vector3.up, true, .075f);
                StartCoroutine (LaunchingSide (false, .075f, .085f));
            }
            else
            {
                Launch (Vector3.up, .09f);
                StartCoroutine (LaunchingSide (true, .09f, .085f));
                StartCoroutine (LaunchingSide (false, .09f, .1f));
            }
        }
    }

    IEnumerator LaunchingSide (bool isLeft, float deltaSpace, float wait)
    {
        yield return new WaitForSeconds (wait);
        LaunchSide (Vector3.up, isLeft, deltaSpace);
    }

    void LaunchSide (Vector3 direction, bool isLeft, float deltaSpace)
    {
        var projPoint = new Vector3 (_projectilePoint.position.x + (isLeft?deltaSpace: -deltaSpace), _projectilePoint.position.y, _projectilePoint.position.z);
        var tpw = Instantiate<ThePrayerWord> (_theWordPrefab, projPoint, Quaternion.identity);
        tpw.speed = speed;
        tpw.damage = damage;
        tpw.direction = direction;
        Destroy (tpw.gameObject, _lifetime);
    }

    void Launch (Vector3 direction, float deltaSpace)
    {
        var tpw = Instantiate<ThePrayerWord> (_theWordPrefab, _projectilePoint.position, Quaternion.identity);
        tpw.speed = speed;
        tpw.damage = damage;
        tpw.direction = direction;
        Destroy (tpw.gameObject, _lifetime);
    }

    IEnumerator WaitingShieldUsingDone ()
    {
        yield return new WaitForSeconds (_shieldScaleTime / _shieldNumber);
        _usingShield = false;
    }

    void OnTriggerEnter (Collider other)
    {
        if ("Enemy".Equals (other.tag))
        {
            if (_activatedShield)
            {
                if (_usingShield) return;
                _usingShield = true;
                StartCoroutine (WaitingShieldUsingDone ());
                var shield = Instantiate<ThePrayerShield> (_theShieldPrefab, _theShieldPoint.position, Quaternion.identity);
                shield.transform.localScale = Vector3.one * _shieldScale;
                shield.damage = _shieldDamage;
                shield.scaleTime = _shieldScaleTime;
            }
        }
    }
}
