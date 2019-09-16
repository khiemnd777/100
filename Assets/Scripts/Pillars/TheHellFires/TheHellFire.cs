using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheHellFire : MonoBehaviour
{
    [SerializeField]
    float _defaultStartY = -4.68f;
    [SerializeField]
    float _stepUp = .25f;
    float _currentStartY = 0f;
    Settings _settings;
    TheHouse _theHouse;

    void Awake ()
    {
        _settings = FindObjectOfType<Settings> ();
        _theHouse = FindObjectOfType<TheHouse> ();
    }

    void Start ()
    {
        transform.position = new Vector3 (transform.position.x, _defaultStartY, transform.position.z);
        _currentStartY = _defaultStartY;
    }

    public void OnEntered ()
    {
        if (_theHouse.sheep == 0)
        {
            _settings.GameOver ();
        }
        _currentStartY += _stepUp;
        StopCoroutine ("GoUp");
        StartCoroutine ("GoUp");
    }

    IEnumerator GoUp ()
    {
        var t = 0f;
        var currentPos = transform.position;
        var destPos = new Vector3 (currentPos.x, _currentStartY, currentPos.z);
        while (t <= 1f)
        {
            transform.position = Vector3.Lerp (currentPos, destPos, t);
            t += Time.deltaTime * 2.5f;
            yield return null;
        }
    }

    public void GoToStartPoint (float second)
    {
        StopCoroutine ("GoUp");
        StartCoroutine (GoingToStartPoint (second));
    }

    IEnumerator GoingToStartPoint (float second)
    {
        var t = 0f;
        var currentPos = transform.position;
        var startPoint = new Vector3 (currentPos.x, _defaultStartY, currentPos.z);
        while (t <= 1f)
        {
            transform.position = Vector3.Lerp (currentPos, startPoint, t);
            t += Time.deltaTime / second;
            yield return null;
        }
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.tag == "Death Point")
        {
            _settings.GameOver ();
        }
    }
}
