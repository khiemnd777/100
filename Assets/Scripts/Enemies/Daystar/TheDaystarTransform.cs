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
    Sprite _lastTransformAppeareance;
    [SerializeField]
    Transform _blastLight;
    [SerializeField]
    SpriteRenderer _blastLightDisplay;
    [SerializeField]
    TheTrueDaystar _theTrueDaystarPrefab;
    [Header ("Earthquake")]
    [SerializeField]
    Earthquake _earthquakePrefab;
    [SerializeField]
    float _earthquakeDuration;
    [SerializeField]
    float _earthquakeMagnitude;
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
        StartCoroutine ("Shake");
        yield return StartCoroutine (ChangeAppearance ());
        yield return StartCoroutine (BlastLight ());
        StopCoroutine ("Shake");
        _display.sprite = _lastTransformAppeareance;
        Instantiate (_theTrueDaystarPrefab, transform.position, Quaternion.identity);
        Earthquake ();
        yield return StartCoroutine (DissolveBlastLight ());
        Destroy (gameObject);
    }

    IEnumerator Shake ()
    {
        while (gameObject)
        {
            _shake.Shake ();
            yield return new WaitForSeconds (_shake.duration);
        }
    }

    void Earthquake ()
    {
        var ins = Instantiate<Earthquake> (_earthquakePrefab, transform.position, Quaternion.identity);
        ins.duration = _earthquakeDuration;
        ins.magnitude = _earthquakeMagnitude;
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
            t += Time.deltaTime / 2f;
            _blastLight.localScale = Vector3.Lerp (Vector3.zero, Vector3.one * 100, t);
            yield return null;
        }
    }

    IEnumerator DissolveBlastLight ()
    {
        var t = 0f;
        while (t <= 1f)
        {
            t += Time.deltaTime / 1f;
            _blastLightDisplay.color = Color32.Lerp (new Color32 (255, 255, 255, 255), new Color32 (255, 255, 255, 0), t);
            yield return null;
        }
    }
}
