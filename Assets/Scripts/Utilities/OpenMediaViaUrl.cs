using UnityEngine;
using UnityEngine.UI;

public class OpenMediaViaUrl : MonoBehaviour
{
    [SerializeField]
    string _url;
    [SerializeField]
    Button _openUrlButton;

    void Awake ()
    {
        _openUrlButton.onClick.AddListener (OpenUrl);
    }

    public void OpenUrl ()
    {
        Application.OpenURL (_url);
    }
}
