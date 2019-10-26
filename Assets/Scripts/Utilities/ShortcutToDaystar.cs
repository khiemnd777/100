using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShortcutToDaystar : MonoBehaviour
{
    [SerializeField]
    float _dueSeconds;
    [SerializeField]
    string _defaultScene;
    float _dueTimeNormalize;
    Settings _settings;

    void Awake ()
    {
        _settings = FindObjectOfType<Settings> ();
    }

    void Update ()
    {
        if (_settings.gameOver) return;
        var tilt = Input.acceleration;
        if (tilt.y >= .6f)
        {
            _dueTimeNormalize += Time.deltaTime / _dueSeconds;
            if (_dueTimeNormalize >= 1f)
            {
                SceneManager.LoadScene (string.Format ("Scenes/{0}", _defaultScene));
                _dueTimeNormalize = 0f;
            }
        }
        else
        {
            _dueTimeNormalize = 0f;
        }
    }
}
