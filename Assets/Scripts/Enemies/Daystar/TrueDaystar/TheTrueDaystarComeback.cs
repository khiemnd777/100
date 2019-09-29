using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheTrueDaystarComeback : MonoBehaviour
{
    [SerializeField]
    Sprite[] _appearances;
    [SerializeField]
    SpriteRenderer _display;
    [SerializeField]
    SpriteRenderer _blastLightDisplay;
    TheDaystar _theDaystar;

    void Awake ()
    {
        _theDaystar = Resources.FindObjectsOfTypeAll<TheDaystar> () [0];
    }

    public IEnumerator Comeback ()
    {
        for (var i = 0; i < _appearances.Length; i++)
        {
            _display.sprite = _appearances[i];
            yield return new WaitForSeconds (.25f);
        }
        _blastLightDisplay.enabled = false;
        // DaystarRestore ();
    }

    void DaystarRestore ()
    {
        _theDaystar.RestoreHp ();
        _theDaystar.gameObject.SetActive (true);
        Destroy (gameObject);
    }
}
