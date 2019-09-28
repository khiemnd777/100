using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleLight : MonoBehaviour
{
    public float speed;
    public Vector3 forward;

    void Start ()
    {
        Destroy(gameObject, 2.75f);
    }

    void Update ()
    {
        transform.Translate (forward * speed * Time.deltaTime);
    }
}
