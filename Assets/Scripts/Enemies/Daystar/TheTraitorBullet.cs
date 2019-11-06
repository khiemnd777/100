using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheTraitorBullet : MonoBehaviour
{
    public float damage;
    [System.NonSerialized]
    public Vector3 direction;
    [System.NonSerialized]
    public float speed;
    [SerializeField]
    TheTraitorBulletImpact _impactPrefab;
    Twinkle _twinkle;

    void Awake ()
    {
        _twinkle = GetComponent<Twinkle> ();
    }

    void Start ()
    {
        _twinkle.Play ();
    }

    void Update ()
    {
        transform.Translate (Time.deltaTime * direction * (speed / 10f));
    }

    public void SelfDestruct ()
    {
        Instantiate (_impactPrefab, transform.position, Quaternion.identity);
        Destroy (gameObject);
    }
}
