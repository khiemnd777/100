using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public bool gameOver = false;
    public float horizontalStep = .2f;
    public float swipeSpeed = .075f;
    public float[] specificHorizontalSteps = { .6f, .4f, .2f, 0f };
    public Button play;
    public Button pause;
    [Header ("Game over")]
    [SerializeField]
    GameOver _gameOverPrefab;
    public string defaultScene;
    [Header ("Replay scene")]
    public string replayScene;
    public Button replay;

    int[] _movedStepDirections = {-1, 1 };

    void Awake ()
    {
        // Time.timeScale = defaultScene == "0.1" ? 0f : 1f;
        play.onClick.AddListener (Play);
        pause.onClick.AddListener (Pause);
        play.gameObject.SetActive (false);
        replay.onClick.AddListener (Replay);
    }

    void Play ()
    {
        if(gameOver) return;
        Time.timeScale = 1f;
        play.gameObject.SetActive (false);
        pause.gameObject.SetActive (true);
    }

    void Pause ()
    {
        if(gameOver) return;
        Time.timeScale = 0f;
        play.gameObject.SetActive (true);
        pause.gameObject.SetActive (false);
    }

    void Replay ()
    {
        if(gameOver) return;
        Time.timeScale = 1f;
        SceneManager.LoadScene (string.Format ("Scenes/{0}", replayScene));
    }

    public float GetSpecificHorizontalStep (float[] indicators)
    {
        var movedStepDirection = _movedStepDirections[Random.Range (0, _movedStepDirections.Length)];
        var specificHorizontalStep = indicators[Random.Range (0, indicators.Length)];
        return specificHorizontalStep * movedStepDirection;
    }

    public float GetStepXByIndex (float[] indicators, int index)
    {
        // sample:
        // index:       0 1 2 3 4 
        // refIndex:   0 1 2 1 0
        var lastIndex = indicators.Length - 1;
        if (index <= lastIndex) return -indicators[index];
        var refIndex = lastIndex - (index - lastIndex);
        return indicators[refIndex];
    }

    public void GameOver ()
    {
        gameOver = true;
        Time.timeScale = 0f;
        Instantiate (_gameOverPrefab, Vector3.zero, Quaternion.identity);
    }

    void PlayAgain ()
    {
        if (string.IsNullOrEmpty (defaultScene)) return;
        SceneManager.LoadScene (string.Format ("Scenes/{0}", defaultScene));
        Time.timeScale = 1f;
    }

    void StartGame ()
    {
        // if(string.IsNullOrEmpty(defaultScene)) return;
        // SceneManager.LoadScene(string.Format("Scenes/{0}", defaultScene));
        Time.timeScale = 1f;
    }
}
