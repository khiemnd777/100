﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public bool gameOver = false;
    public float horizontalStep = .2f;
    public float[] specificHorizontalSteps = { .6f, .4f, .2f, 0f };
    [Header ("Game Over")]
    public string defaultScene;
    public Text oopsText;
    public Button playAgainButton;
    public Button startButton;

    int[] _movedStepDirections = {-1, 1 };

    void Awake ()
    {
        Time.timeScale = 0f;
        playAgainButton.onClick.AddListener (PlayAgain);
        startButton.onClick.AddListener (StartGame);
        playAgainButton.gameObject.SetActive (false);
        oopsText.gameObject.SetActive (false);
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
        playAgainButton.gameObject.SetActive (true);
        oopsText.gameObject.SetActive (true);
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
        startButton.gameObject.SetActive (false);
    }
}
