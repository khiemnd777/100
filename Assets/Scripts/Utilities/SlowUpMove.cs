using System.Linq;
using UnityEngine;

public class SlowUpMove : MonoBehaviour
{
    [SerializeField]
    float _normalSpeed;
    [SerializeField]
    float _slowedSpeed;
    [SerializeField]
    SettingData _settingData;

    Settings _settings;

    void Awake ()
    {
        _settings = FindObjectOfType<Settings> ();
    }

    void Update ()
    {
        if (!_settingData.slowmotion) return;
        if (!_settings.activedSlowUpMove) return;
        if (_settings.gameOver) return;
        if (_settings.isPause) return;
        if (Input.touches.Any (x => x.phase == TouchPhase.Began))
        {
            Time.timeScale = _normalSpeed;
        }
        else if (Input.touches.All (x => x.phase == TouchPhase.Ended))
        {
            Time.timeScale = _slowedSpeed;
        }
    }
}
