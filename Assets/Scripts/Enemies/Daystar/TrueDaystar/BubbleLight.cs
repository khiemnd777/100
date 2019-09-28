using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleLight : MonoBehaviour
{
    public float speed;
    public int direction;
    public float destructTime;
    TheTrueDaystar _theTrueDaystar;

    void Awake ()
    {
        _theTrueDaystar = FindObjectOfType<TheTrueDaystar> ();
    }

    void Start ()
    {
        Destroy (gameObject, destructTime);
    }

    void Update ()
    {
        var forward = (_theTrueDaystar.transform.position - transform.position) * direction;
        forward.Normalize();
        transform.Translate (forward * speed * Time.deltaTime);
    }
}
