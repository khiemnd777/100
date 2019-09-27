using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheDaystarTransform : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer _display;
    [SerializeField]
    Sprite[] _appeareances;
    [SerializeField]
    Transform _blastLight;
    ObjectShake _shake;

    void Awake ()
    {
        _shake = GetComponent<ObjectShake> ();
        _blastLight.localScale = Vector3.zero;
    }

    void Start ()
    {
        StartCoroutine (Script ());
    }

    IEnumerator Script ()
    {
        StartCoroutine (Shake ());
        yield return StartCoroutine (ChangeAppearance ());
        yield return StartCoroutine (BlastLight ());

    }

    IEnumerator Shake ()
    {
        while (gameObject)
        {
            _shake.Shake ();
            yield return new WaitForSeconds (_shake.duration);
        }
    }

    IEnumerator ChangeAppearance ()
    {
        var currentAppearanceState = 0;
        while (currentAppearanceState < _appeareances.Length)
        {
            yield return new WaitForSeconds (1f);
            _display.sprite = _appeareances[currentAppearanceState];
            ++currentAppearanceState;
        }
    }

    IEnumerator BlastLight ()
    {
        var t = 0f;
        while (t <= 1f)
        {
            t += Time.deltaTime / 3.25f;
            _blastLight.localScale = Vector3.Lerp (Vector3.zero, Vector3.one * 100, t);
            yield return null;
        }
    }
}
