﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeTutorial : MonoBehaviour
{
    [SerializeField]
    float _disappearanceSeconds;
    [SerializeField]
    SpriteRenderer _renderer;
    [SerializeField]
    SettingData _settingData;

    void Start ()
    {
        if (!_settingData.alreadySwipeTutorial)
        {
            _settingData.alreadySwipeTutorial = true;
            _renderer.enabled = true;
            StartCoroutine (Disappearance ());
            return;
        }
        _renderer.enabled = false;
    }

    IEnumerator Disappearance ()
    {
        yield return new WaitForSeconds (_disappearanceSeconds);
        var t = 0f;
        while (t <= 1f)
        {
            t += Time.deltaTime / 1f;
            _renderer.color = Color32.Lerp (new Color32 (255, 255, 255, 255), new Color32 (255, 255, 255, 0), t);
            yield return null;
        }
        Destroy (gameObject);
    }
}
