using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheShepherd : MonoBehaviour
{
    [SerializeField]
    float _appearedStartTime = .25f;
    bool _isMoving = false;
    Settings _settings;
    TheHouse _theHouse;
    [SerializeField]
    DissolveEffect _dissolvedEffectOnHit;

    void Awake ()
    {
        _settings = FindObjectOfType<Settings> ();
        _theHouse = FindObjectOfType<TheHouse> ();
    }

    void Start ()
    {
        StartCoroutine (AppearedAtStart ());
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
        Instantiate (_dissolvedEffectOnHit, Vector3.up * 1.01f, Quaternion.identity);
        _theHouse.OnInfected ();
        _settings.Vibrate ();
        if (_theHouse.sheep <= 0)
        {
            _settings.GameOver ();
        }
    }

    public void Hit (int damage)
    {
        Instantiate (_dissolvedEffectOnHit, Vector3.up * 1.01f, Quaternion.identity);
        for (var i = 0; i < damage; i++)
        {
            _theHouse.OnInfected ();
            _settings.Vibrate ();
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
}
