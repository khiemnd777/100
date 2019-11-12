using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Luke1546SceneManager : MonoBehaviour
{
    [SerializeField]
    string _nextScene;
    [SerializeField]
    LogoAppearance _logoAppearance;

    void Start ()
    {
        StartCoroutine (Scripting ());
    }

    IEnumerator Scripting ()
    {
        yield return StartCoroutine (_logoAppearance.Queue ());
        SceneManager.LoadScene (string.Format ("Scenes/{0}", _nextScene));
    }
}
