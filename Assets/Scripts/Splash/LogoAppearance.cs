using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogoAppearance : MonoBehaviour
{
    [SerializeField]
    float _speed;
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
        StartCoroutine (Appear ());
    }

    IEnumerator Appear ()
    {
        yield return new WaitForSeconds (1f);
        var t = 0f;
        var src = new Color32 (255, 255, 255, 0);
        var dest = new Color32 (255, 255, 255, 255);
        while (t <= 1f)
        {
            t += Time.deltaTime * _speed;
            _display.color = Color.Lerp (src, dest, t);
            yield return null;
        }
        yield return new WaitForSeconds(1.25f);
        SceneManager.LoadScene (string.Format ("Scenes/{0}", _defaultScene));
    }
}
