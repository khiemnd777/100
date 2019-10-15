using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TheDaystarTransform : MonoBehaviour
{
    public bool permanent;
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
    [SerializeField]
    SpriteRenderer _theEndBackgroundPrefab;
    [SerializeField]
    SpriteRenderer _addOilPrefab;
    ObjectShake _shake;
    Earthquake _earthquake;
    TheHellFire _theHellFire;
    Settings _settings;

    void Awake ()
    {
        _shake = GetComponent<ObjectShake> ();
        _blastLight.localScale = Vector3.zero;
        _earthquake = FindObjectOfType<Earthquake> ();
        _theHellFire = FindObjectOfType<TheHellFire> ();
        _settings = FindObjectOfType<Settings> ();
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
        _display.sprite = permanent ? null : _lastTransformAppeareance;
        _earthquake.StartEarthquake ();
        if (!permanent)
        {
            InitTrueDaystar ();
            yield return StartCoroutine (DissolveBlastLight ());
        }
        else
        {
            _theHellFire.transform.position = new Vector3 (0f, 16f, 0f);
            yield return StartCoroutine (DissolveBlastLight ());
            _earthquake.StopEarthquake ();
            yield return StartCoroutine (EndGame ());
        }
        Destroy (gameObject);
    }

    IEnumerator EndGame ()
    {
        var theEndBg = Instantiate<SpriteRenderer> (_theEndBackgroundPrefab, Vector3.zero, Quaternion.identity);
        yield return new WaitForSeconds (1.5f);
        var t = 0f;
        var ca = new Color32 (255, 255, 255, 0);
        var cb = new Color32 (255, 255, 255, 255);
        while (t <= 1f)
        {
            t += Time.deltaTime / 2f;
            theEndBg.color = Color32.Lerp (ca, cb, t);
            yield return null;
        }
        t = 0f;
        var addOil = Instantiate<SpriteRenderer> (_addOilPrefab, new Vector3 (0f, .9f, 0f), Quaternion.identity);
        while (t <= 1f)
        {
            t += Time.deltaTime / 1.5f;
            addOil.color = Color32.Lerp (ca, cb, t);
            yield return null;
        }
        yield return new WaitForSeconds (3f);
        t = 0f;
        while (t <= 1f)
        {
            t += Time.deltaTime / 1.5f;
            addOil.color = Color32.Lerp (cb, ca, t);
            yield return null;
        }
        yield return new WaitForSeconds (1.25f);
        SceneManager.LoadScene (string.Format ("Scenes/{0}", "Tap to play"));
    }

    void InitTrueDaystar ()
    {
        Instantiate (_theTrueDaystarPrefab, transform.position, Quaternion.identity);
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
