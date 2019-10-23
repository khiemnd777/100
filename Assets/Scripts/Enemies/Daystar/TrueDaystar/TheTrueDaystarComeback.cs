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
    [SerializeField]
    AnimationCurve _curveAnimThatPosition;
    TheDaystar _theDaystar;
    TheHouse _theHouse;
    Settings _settings;

    void Awake ()
    {
        _theDaystar = Resources.FindObjectsOfTypeAll<TheDaystar> () [0];
        _theHouse = FindObjectOfType<TheHouse> ();
        _settings = FindObjectOfType<Settings> ();
    }

    public IEnumerator Comeback ()
    {
        StartCoroutine (MoveToThatPosition ());
        for (var i = 0; i < _appearances.Length; i++)
        {
            _display.sprite = _appearances[i];
            yield return new WaitForSeconds (.25f);
        }
        _blastLightDisplay.enabled = false;
        // yield return StartCoroutine (MoveToThatPosition ());
        DaystarRestore ();
        if (_theHouse.sheep <= 0)
        {
            _settings.GameOver ();
        }
    }

    void DaystarRestore ()
    {
        _theDaystar.RestoreHp ();
        _theDaystar.gameObject.SetActive (true);
        _theDaystar.ExecuteSkill ();
        Destroy (gameObject);
    }

    IEnumerator MoveToThatPosition ()
    {
        if (transform.position.y > 3.77f)
        {
            var srcPos = transform.position;
            var destPos = new Vector3 (srcPos.x, 3.77f, srcPos.z);
            var t = 0f;
            while (t <= 1f)
            {
                t += Time.deltaTime / 2f;
                transform.position = Vector3.Lerp (srcPos, destPos, _curveAnimThatPosition.Evaluate (t));
                yield return null;
            }
        }
    }
}
