using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheTrueDaystarComeback : MonoBehaviour
{
    [SerializeField]
    Sprite[] _appearances;
    [SerializeField]
    SpriteRenderer _display;
    TheDaystar _theDaystar;

    void Awake ()
    {
        _theDaystar = Resources.FindObjectsOfTypeAll<TheDaystar> () [0];
        Debug.Log (_theDaystar);
    }

    public IEnumerator Comeback ()
    {
        for (var i = 0; i < _appearances.Length; i++)
        {
            _display.sprite = _appearances[i];
            yield return new WaitForSeconds (.25f);
        }
        _theDaystar.RestoreHp ();
        _theDaystar.gameObject.SetActive (true);
        Destroy (gameObject);
    }
}
