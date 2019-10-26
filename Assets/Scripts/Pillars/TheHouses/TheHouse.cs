using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TheHouse : MonoBehaviour
{
    public Transform goOutPoint;
    public Transform theShepherdPoint;
    public int sheep = 1;
    public Transform initSheepPoint;
    public const int maxSheep = 100;
    [SerializeField]
    Sprite[] _appearances;
    [SerializeField]
    SpriteRenderer _display;
    [SerializeField]
    TextMesh _100txt;
    [SerializeField]
    string _nextScene;
    [SerializeField]
    SettingData _settingData;
    int _currentAppearanceState;
    Settings _settings;
    TheHellFire _hellFire;
    TheLight _theLight;

    void Awake ()
    {
        _settings = FindObjectOfType<Settings> ();
        _hellFire = FindObjectOfType<TheHellFire> ();
        _theLight = FindObjectOfType<TheLight> ();
        _currentAppearanceState = sheep == 100 ? _appearances.Length - 1 : 0;
    }

    void Update ()
    {
        _100txt.text = sheep.ToString ();
    }

    public void OnConverted ()
    {
        sheep = sheep >= maxSheep ? maxSheep : sheep + 1;
        ChangeAppearanceAsIncrease ();
        CollectEnough (sheep);
    }

    public void OnInfected ()
    {
        sheep = sheep <= 0 ? 0 : sheep - 1;
        ChangeAppearanceAsDecrease ();
        // if (sheep <= 0)
        // {
        //     _settings.GameOver ();
        // }
    }

    public void OnHealed ()
    {
        sheep = sheep >= maxSheep ? maxSheep : sheep + 1;
        ChangeAppearanceAsIncrease ();
        CollectEnough (sheep);
    }

    void CollectEnough (int sheep)
    {
        if (sheep < 100) return;
        _hellFire.GoToStartPoint (.5f);
        _theLight.SelfDestructTheStars ();
        StartCoroutine (GotoNextScene (_nextScene));
    }

    IEnumerator GotoNextScene (string scene)
    {
        if (string.IsNullOrEmpty (scene)) yield break;
        yield return new WaitForSeconds (.5f);
        var loadScene = _settingData.killedCount >= 7000 ? "Daystar appear" : scene;
        SceneManager.LoadScene (string.Format ("Scenes/{0}", loadScene));
    }

    void ChangeAppearanceAsIncrease ()
    {
        if (_currentAppearanceState < 0)
        {
            _currentAppearanceState = 0;
        }
        var normalizedHp = sheep / 100f;
        var ratio = 1f / _appearances.Length * (_currentAppearanceState + 1);
        if (normalizedHp > ratio)
        {
            _display.sprite = _appearances[_currentAppearanceState];
            ++_currentAppearanceState;
        }
    }

    void ChangeAppearanceAsDecrease ()
    {
        if (_currentAppearanceState < 0)
        {
            _currentAppearanceState = 0;
        }
        var normalizedHp = sheep / 100f;
        var ratio = 1f / _appearances.Length * (_currentAppearanceState + 1);
        if (normalizedHp <= ratio)
        {
            _display.sprite = _appearances[_currentAppearanceState];
            --_currentAppearanceState;
        }
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.tag == "The Sheep")
        {
            var theSheep = other.GetComponent<TheSheep> ();
            if (theSheep.target.tag == "The House")
            {
                ++sheep;
                theSheep.SelfDestruct ();
            }
        }
    }
}
