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

    // Update is called once per frame
    void FixedUpdate ()
    {
        transform.Translate (Time.fixedDeltaTime * direction * (speed / 10f));
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
}
