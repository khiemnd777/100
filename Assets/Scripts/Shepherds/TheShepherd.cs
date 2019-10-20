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
    SwipeDetector _swipeDetector;

    void Awake ()
    {
        _settings = FindObjectOfType<Settings> ();
        _theHouse = FindObjectOfType<TheHouse> ();
        _swipeDetector = GetComponent<SwipeDetector> ();
        _swipeDetector.OnSwipe += OnSwipe;
    }

    void Start ()
    {
        StartCoroutine (AppearedAtStart ());
    }

    void Update ()
    {
        if (Input.GetKeyDown (KeyCode.A))
        {
            DirectedSwipe (-1);
        }
        else if (Input.GetKeyDown (KeyCode.D))
        {
            DirectedSwipe (1);
        }
    }

    void OnSwipe (SwipeData data)
    {
        if (_settings.gameOver) return;
        if (!_isMoving) return;
        switch (data.Direction)
        {
            case SwipeDirection.Left:
                if (_smoothMoving)
                {
                    StartCoroutine (DirectedSwiping (-1));
                }
                else
                {
                    DirectedSwipe (-1);
                }
                break;
            case SwipeDirection.Right:
                if (_smoothMoving)
                {
                    StartCoroutine (DirectedSwiping (1));
                }
                else
                {
                    DirectedSwipe (1);
                }
                break;
            default:
                break;
        }
    }

    IEnumerator DirectedSwiping (int direction)
    {
        _isMoving = false;
        var horizontalStep = _settings.horizontalStep;
        var start = transform.position;
        var end = new Vector3 (start.x + horizontalStep * direction, start.y, start.z);
        var t = 0f;
        while (t <= 1f)
        {
            t += Time.deltaTime / .075f;
            transform.position = Vector3.Lerp (start, end, t);
            yield return null;
        }
        transform.position = end;
        _isMoving = true;
    }

    void DirectedSwipe (int direction)
    {
        var start = transform.position;
        var horizontalStep = _settings.horizontalStep;
        var maxStep = _settings.specificHorizontalSteps[0];
        var endX = start.x + horizontalStep * direction;
        var realEndX = Mathf.Clamp (endX, -maxStep, maxStep);
        var end = new Vector3 (realEndX, start.y, start.z);
        transform.position = end;
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
