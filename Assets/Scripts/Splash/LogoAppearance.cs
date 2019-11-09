using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogoAppearance : MonoBehaviour
{
    [SerializeField]
    float _waitAppearSeconds = 1f;
    [SerializeField]
    float _appearSeconds;
    [SerializeField]
    float _displaySeconds;
    [SerializeField]
    bool _enableDisappear;
    [SerializeField]
    float _disappearSeconds;
    [SerializeField]
    float _nextSeconds = 1.25f;
    [SerializeField]
    SpriteRenderer _display;
    [SerializeField]
    string _defaultScene;

    void Awake ()
    {
        _display.color = new Color32 (255, 255, 255, 0);
    }

    void Start ()
    {
        StartCoroutine (Queue ());
    }

    IEnumerator Queue ()
    {
        yield return StartCoroutine (Appear ());
        if (_enableDisappear)
        {
            yield return StartCoroutine (Disappear ());
        }
        yield return new WaitForSeconds (_nextSeconds);
        SceneManager.LoadScene (string.Format ("Scenes/{0}", _defaultScene));
    }

    IEnumerator Appear ()
    {
        yield return new WaitForSeconds (_waitAppearSeconds);
        var t = 0f;
        var src = new Color32 (255, 255, 255, 0);
        var dest = new Color32 (255, 255, 255, 255);
        while (t <= 1f)
        {
            t += Time.unscaledDeltaTime / _appearSeconds;
            _display.color = Color.Lerp (src, dest, t);
            yield return null;
        }
        yield return new WaitForSeconds (_displaySeconds);
    }

    IEnumerator Disappear ()
    {
        var t = 0f;
        var src = new Color32 (255, 255, 255, 255);
        var dest = new Color32 (255, 255, 255, 0);
        while (t <= 1f)
        {
            t += Time.unscaledDeltaTime / _disappearSeconds;
            _display.color = Color.Lerp (src, dest, t);
            yield return null;
        }
    }
}
