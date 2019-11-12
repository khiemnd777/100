using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashSceneManager : MonoBehaviour
{
    [SerializeField]
    string _nextScene;
    [SerializeField]
    LogoAppearance _logoAppearance;
    [SerializeField]
    SpriteRenderer _blackBg;

    void Start ()
    {
        StartCoroutine (Scripting ());
    }

    IEnumerator Scripting ()
    {
        yield return StartCoroutine (_logoAppearance.Queue ());
        yield return StartCoroutine (BlackBackgroundDisappear ());
        SceneManager.LoadScene (string.Format ("Scenes/{0}", _nextScene));
    }

    IEnumerator BlackBackgroundDisappear ()
    {
        var t = 0f;
        var ca = new Color32 (255, 255, 255, 0);
        var cb = new Color32 (255, 255, 255, 255);
        while (t <= 1f)
        {
            t += Time.deltaTime / 1f;
            _blackBg.color = Color32.Lerp (ca, cb, t);
            yield return null;
        }
    }
}
