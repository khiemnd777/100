using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitOrReplay : MonoBehaviour
{
    public Button playAgainButton;
    public Button exitButton;
    public Button backButton;
    public string playAgainScene;
    public string exitScene;
    Settings _settings;

    void Awake ()
    {
        _settings = FindObjectOfType<Settings> ();
        playAgainButton.onClick.AddListener (PlayAgain);
        exitButton.onClick.AddListener (Exit);
        backButton.onClick.AddListener (Back);
    }

    void PlayAgain ()
    {
        if (string.IsNullOrEmpty (playAgainScene)) return;
        Time.timeScale = 1f;
        SceneManager.LoadScene (string.Format ("Scenes/{0}", playAgainScene));
    }

    void Exit ()
    {
        if (string.IsNullOrEmpty (exitScene)) return;
        Time.timeScale = 1f;
        SceneManager.LoadScene (string.Format ("Scenes/{0}", exitScene));
    }

    void Back ()
    {
        _settings.Play ();
        gameObject.SetActive (false);
    }
}
