using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialSkillTutorial : MonoBehaviour
{
    [SerializeField]
    float _disappearanceSeconds;
    [SerializeField]
    SpriteRenderer _renderer;
    [SerializeField]
    SettingData _settingData;
    TheHouse _theHouse;

    void Awake ()
    {
        _theHouse = FindObjectOfType<TheHouse> ();
    }

    void Start ()
    {
        if (!_settingData.alreadySpecialSkillTutorial)
        {
            StartCoroutine (CheckAbove50 ());
            return;
        }
        _renderer.enabled = false;
    }

    IEnumerator CheckAbove50 ()
    {
        while (_theHouse.sheep < 50)
        {
            yield return null;
        }
        _settingData.alreadySpecialSkillTutorial = true;
        _renderer.enabled = true;
        StartCoroutine (Disappearance ());
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
