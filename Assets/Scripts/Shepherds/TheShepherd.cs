using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheShepherd : MonoBehaviour
{
    [SerializeField]
    float _appearedStartTime = .25f;
    bool _isMoving = false;
    [SerializeField]
    bool _smoothMoving;
    Settings _settings;
    TheHouse _theHouse;
    [SerializeField]
    Camera _theCamera;
    Rigidbody _rd;

    void Awake ()
    {
        _settings = FindObjectOfType<Settings> ();
        _theHouse = FindObjectOfType<TheHouse> ();
        _rd = GetComponent<Rigidbody> ();
    }

    void Start ()
    {
        StartCoroutine (AppearedAtStart ());
    }

    void Update ()
    {
        foreach (var touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Moved)
            {
                _rd.velocity = Vector3.right * touch.deltaPosition.x / 11f;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                _rd.velocity = Vector3.zero;
            }
        }
    }

    IEnumerator AppearedAtStart ()
    {
        _isMoving = false;
        var t = 0f;
        var start1 = transform.position;
        var end1 = _theHouse.theShepherdPoint.position + Vector3.up * .2f;
        while (t <= 1f)
        {
            t += Time.deltaTime / (_appearedStartTime * .75f);
            transform.position = Vector3.Lerp (start1, end1, t);
            yield return null;
        }
        t = 0f;
        var end2 = _theHouse.theShepherdPoint.position;
        while (t <= 1f)
        {
            t += Time.deltaTime / (_appearedStartTime * .25f);
            transform.position = Vector3.Lerp (end1, end2, t);
            yield return null;
        }
        transform.position = end2;
        _isMoving = true;
    }

    public void Hit ()
    {
        _theHouse.OnInfected ();
        Handheld.Vibrate ();
        if (_theHouse.sheep <= 0)
        {
            _settings.GameOver ();
        }
    }

    public void Hit (int damage)
    {
        for (var i = 0; i < damage; i++)
        {
            _theHouse.OnInfected ();
            Handheld.Vibrate ();
            if (_theHouse.sheep <= 0)
            {
                _settings.GameOver ();
                return;
            }
        }
    }

    public void SelfDestruct ()
    {
        Destroy (gameObject);
    }

    // void OnTriggerEnter (Collider other)
    // {
    //     if ("Daystar Attacker".Equals (other.tag))
    //     {
    //         Debug.Log (other.name);
    //         _theHouse.OnInfected ();
    //     }
    // }
}
