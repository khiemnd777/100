using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheHouseEnergyRadioactive : MonoBehaviour
{
    void Start ()
    {
        StartCoroutine (ScaleOut ());
    }

    IEnumerator ScaleOut ()
    {
        var t = 0f;
        while (t <= 1f)
        {
            t += Time.deltaTime / .5f;
            transform.localScale = Vector3.Lerp (Vector3.one, Vector3.one * 30f, t);
            yield return null;
        }
        Destroy (gameObject);
    }

    void OnTriggerEnter (Collider other)
    {
        if ("Enemy".Equals (other.tag))
        {
            var theStar = other.GetComponent<TheStar> ();
            if (theStar)
            {
                theStar.SelfDestruct ();
                Handheld.Vibrate ();
            }
        }
    }
}
