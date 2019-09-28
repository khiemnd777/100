using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleLight : MonoBehaviour
{
    public float speed;
    public Vector3 forward;
    public float destructTime;

    void Start ()
    {
        Destroy(gameObject, destructTime);
    }

    void Update ()
    {
        transform.Translate (forward * speed * Time.deltaTime);
    }
}
