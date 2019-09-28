using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheSheep : MonoBehaviour
{
    public float speed;
    [SerializeField]
    SpriteRenderer _display;
    TheTrueDaystar _theTrueDaystar;

    void Awake ()
    {
        _theTrueDaystar = FindObjectOfType<TheTrueDaystar> ();
    }

    void Start ()
    {
        StartCoroutine (Twinkle ());
    }

    void Update ()
    {
        var forward = _theTrueDaystar.transform.position - transform.position;
        forward.Normalize();
        transform.Translate (forward * speed * Time.deltaTime);
    }

    IEnumerator Twinkle ()
    {
        var scale = _display.transform.localScale.x;
        while (gameObject)
        {
            _display.transform.localScale = Vector3.one * Random.Range (scale / 1.65f, scale);
            yield return null;
        }
    }

    public void SelfDestruct ()
    {
        Destroy (gameObject);
    }
}
