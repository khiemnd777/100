using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThePrayer : MonoBehaviour
{
    public float damage;
    public float speed;
    public float delayBetweenLaunch;
    [SerializeField]
    float _lifetime;
    [SerializeField]
    Transform _projectilePoint;
    [SerializeField]
    ThePrayerWord _theWordPrefab;
    [SerializeField]
    LayerMask _detectedLayer;
    float _tDelay = 1f;

    void Start ()
    {
        // StartCoroutine (Launching ());
    }

    void Update ()
    {
        if (_tDelay <= 1f)
        {
            _tDelay += Time.deltaTime / delayBetweenLaunch;
            return;
        }
        _tDelay = 0f;
        RaycastHit hit;
        if (Physics.Raycast (transform.position, Vector3.up, out hit, float.MaxValue, _detectedLayer))
        {
            Launch ();
        }
    }

    IEnumerator Launching ()
    {
        while (gameObject)
        {
            Launch ();
            yield return new WaitForSeconds (delayBetweenLaunch);
        }
    }

    void Launch ()
    {
        var tpw = Instantiate<ThePrayerWord> (_theWordPrefab, _projectilePoint.position, Quaternion.identity);
        tpw.speed = speed;
        tpw.damage = damage;
        tpw.direction = Vector3.up;
        Destroy (tpw.gameObject, _lifetime);
    }
}
