﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public SettingData settingData;
    public bool activedSlowUpMove;
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
    [SerializeField]
    VideoAds _videoAds;
    [Header ("Replay scene")]
    public Button replay;
    [SerializeField]
    ExitOrReplay _exitOrReplay;
    [Header ("Setting")]
    public Button setting;
    [SerializeField]
    SettingPanel _settingPanel;
    [System.NonSerialized]
    public bool isPause;

    int[] _movedStepDirections = {-1, 1 };

    void Awake ()
    {
        // Time.timeScale = defaultScene == "0.1" ? 0f : 1f;
        if (play) play.onClick.AddListener (Play);
        if (pause) pause.onClick.AddListener (Pause);
        if (play) play.gameObject.SetActive (false);
        if (replay) replay.onClick.AddListener (Replay);
        if (setting) setting.onClick.AddListener (ShowSetting);
    }

    public void Play ()
    {
        if (gameOver) return;
        isPause = false;
        Time.timeScale = 1f;
        play.gameObject.SetActive (false);
        pause.gameObject.SetActive (true);
    }

    public void Pause ()
    {
        if (gameOver) return;
        isPause = true;
        Time.timeScale = 0f;
        play.gameObject.SetActive (true);
        pause.gameObject.SetActive (false);
    }

    public void Vibrate ()
    {
        if (!settingData.vibrated) return;
        Handheld.Vibrate ();
    }

    void Replay ()
    {
        if (gameOver) return;
        SetInteractableButtons (false);
        Pause ();
        _exitOrReplay.gameObject.SetActive (true);
    }

    void ShowSetting ()
    {
        if (gameOver) return;
        SetInteractableButtons (false);
        Pause ();
        _settingPanel.gameObject.SetActive (true);
    }

    public void ActiveSlowUpMove (bool actived)
    {
        activedSlowUpMove = actived;
        if (!actived)
        {
            Time.timeScale = 1f;
        }
    }

    public void SetInteractableButtons (bool interactable)
    {
        play.interactable = interactable;
        pause.interactable = interactable;
        replay.interactable = interactable;
        setting.interactable = interactable;
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
        var gameOverPanel = Instantiate<GameOver> (_gameOverPrefab, Vector3.zero, Quaternion.identity);
        gameOverPanel.videoAds = _videoAds;
        gameOverPanel.defaultScene = defaultScene;
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
