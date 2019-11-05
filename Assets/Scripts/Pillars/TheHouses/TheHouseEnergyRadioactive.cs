using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheHouseEnergyRadioactive : MonoBehaviour
{
    Settings _settings;

    void Awake ()
    {
        _settings = FindObjectOfType<Settings> ();
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
                theStar.OnHit (9999, transform);
                _settings.Vibrate ();
            }
        }
        else if ("Daystar".Equals (other.tag))
        {
            var theDaystar = other.GetComponent<TheDaystar> ();
            if (theDaystar)
            {
                theDaystar.Hit (theDaystar.GetHp () / 2f);
                _settings.Vibrate ();
            }
        }
    }
}
