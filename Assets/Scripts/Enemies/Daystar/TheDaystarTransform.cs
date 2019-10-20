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
    ObjectShake _shake;
    Earthquake _earthquake;
    TheHellFire _theHellFire;
    Settings _settings;
    EndGame _endGame;

    void Awake ()
    {
        _shake = GetComponent<ObjectShake> ();
        _blastLight.localScale = Vector3.zero;
        _earthquake = FindObjectOfType<Earthquake> ();
        _theHellFire = FindObjectOfType<TheHellFire> ();
        _settings = FindObjectOfType<Settings> ();
        _endGame = FindObjectOfType<EndGame> ();
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
        _earthquake.StartEarthquake (true);
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
            yield return StartCoroutine (_endGame.Play ());
        }
        Destroy (gameObject);
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
