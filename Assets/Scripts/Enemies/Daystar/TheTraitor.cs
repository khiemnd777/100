using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheTraitor : MonoBehaviour
{
    public bool isTraitor;
    [SerializeField]
    Twinkle _twinkle;
    [SerializeField]
    TheTraitorBullet _bulletPrefab;

    void Start ()
    {
        if (isTraitor)
        {
            _twinkle.Play ();
        }
    }

    public void InstantiateBullet (float damage)
    {
        var bullet = Instantiate<TheTraitorBullet> (_bulletPrefab, transform.position, Quaternion.identity);
        bullet.speed = 100f;
        bullet.direction = Vector3.up;
        bullet.damage = damage;
    }
}
