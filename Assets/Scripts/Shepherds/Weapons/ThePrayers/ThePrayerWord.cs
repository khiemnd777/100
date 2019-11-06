using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThePrayerWord : MonoBehaviour
{
    [System.NonSerialized]
    public float speed;
    [System.NonSerialized]
    public float damage;
    [System.NonSerialized]
    public Vector3 direction;
    [SerializeField]
    ParticleSystem _hitBlow;
    [SerializeField]
    AudioSource _soundFxAtCollision;

    void Start ()
    {
        InstantiateSoundEffectAtCollision ();
    }

    // Update is called once per frame
    void Update ()
    {
        transform.Translate (Time.deltaTime * direction * (speed / 10f));
    }

    void InstantiateSoundEffectAtCollision ()
    {
        if (!_soundFxAtCollision) return;
        Instantiate (_soundFxAtCollision, transform.position, Quaternion.identity);
    }

    void OnTriggerEnter (Collider other)
    {
        if ("Enemy".Equals (other.tag))
        {
            var enemy = other.GetComponent<Enemy> ();
            enemy.OnHit (damage, transform);
            var hitFx = Instantiate<ParticleSystem> (_hitBlow, other.transform.position, Quaternion.identity);
            Destroy (gameObject);
        }
    }

    public void SelfDestruct ()
    {
        Destroy (gameObject);
    }
}
