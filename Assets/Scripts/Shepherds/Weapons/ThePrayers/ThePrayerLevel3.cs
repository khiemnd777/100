using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThePrayerLevel3 : MonoBehaviour
{
    public float damage;
    public float speed;
    public float delayBetweenLaunch;
    [SerializeField]
    float _lifetime;
    [SerializeField]
    Transform _projectilePoint;
    [SerializeField]
    Transform _subprojectilePoint1;
    [SerializeField]
    Transform _subprojectilePoint2;
    [SerializeField]
    ThePrayerWord _theWordPrefab;
    [SerializeField]
    ThePrayerWord _theSubwordPrefab;

    void Start ()
    {
        StartCoroutine (Launch ());
    }

    IEnumerator Launch ()
    {
        while (gameObject)
        {
            var tpw = Instantiate<ThePrayerWord> (_theWordPrefab, _projectilePoint.position, Quaternion.identity);
            tpw.speed = speed;
            tpw.damage = damage;
            Destroy (tpw.gameObject, _lifetime);
            var stpw1 = Instantiate<ThePrayerWord> (_theSubwordPrefab, _subprojectilePoint1.position, Quaternion.identity);
            stpw1.speed = speed;
            stpw1.damage = damage;
            Destroy (stpw1.gameObject, _lifetime);
            var stpw2 = Instantiate<ThePrayerWord> (_theSubwordPrefab, _subprojectilePoint2.position, Quaternion.identity);
            stpw2.speed = speed;
            stpw2.damage = damage;
            Destroy (stpw2.gameObject, _lifetime);
            yield return new WaitForSeconds (delayBetweenLaunch);
        }
    }
}
