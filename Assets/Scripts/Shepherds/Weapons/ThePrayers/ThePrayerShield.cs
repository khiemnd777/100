﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThePrayerShield : MonoBehaviour
{
    [System.NonSerialized]
    public float scaleTime;
    [System.NonSerialized]
    public float damage;

    void Awake ()
    {
        transform.localScale = Vector3.zero;
    }

    void Start ()
    {
        StartCoroutine (ScaleOut ());
    }

    IEnumerator ScaleOut ()
    {
        var t = 0f;
        while (t <= 1f)
        {
            t += Time.deltaTime / scaleTime;
            transform.localScale = Vector3.Lerp (Vector3.one * .75f, Vector3.one, t);
            yield return null;
        }
        Destroy (gameObject);
    }

    void OnTriggerEnter (Collider other)
    {
        if ("Enemy".Equals (other.tag))
        {
            var enemy = other.GetComponent<Enemy> ();
            enemy.OnHit (damage, transform);
        }
    }
}
