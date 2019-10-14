using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheSheep : MonoBehaviour
{
    public float speed;
    [System.NonSerialized]
    public Transform target;
    [SerializeField]
    SpriteRenderer _display;

    void Start ()
    {
        StartCoroutine (Twinkle ());
    }

    void Update ()
    {
        var forward = target.transform.position - transform.position;
        forward.Normalize ();
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
