using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Button playAgainButton;
    public string defaultScene;

    void Awake ()
    {
        playAgainButton.onClick.AddListener (PlayAgain);
    }

    void PlayAgain ()
    {
        if (string.IsNullOrEmpty (defaultScene)) return;
        SceneManager.LoadScene (string.Format ("Scenes/{0}", defaultScene));
        Time.timeScale = 1f;
        Destroy (gameObject);
    }
}
