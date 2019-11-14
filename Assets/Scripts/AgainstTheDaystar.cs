using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AgainstTheDaystar : MonoBehaviour
{
    [SerializeField]
    SettingData _settingData;
    [SerializeField]
    Button _button;
    [SerializeField]
    Button _againstDaystarDisabledButton;
    [SerializeField]
    string _defaultScene;

    void Awake ()
    {
        _button.onClick.AddListener (() =>
        {
            SceneManager.LoadScene (string.Format ("Scenes/{0}", _defaultScene));
        });
    }

    void Start ()
    {
        _button.gameObject.SetActive (_settingData.unlockTheDaystar);
        _againstDaystarDisabledButton.gameObject.SetActive(!_settingData.unlockTheDaystar);
    }
}
