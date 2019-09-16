using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TheLight : MonoBehaviour
{
    [Space]
    [SerializeField]
    float _defaultStartY = 6.75f;
    [SerializeField]
    TheLightSpawner _spawnerPrefab;
    TheLightSpawner _currentSpawner;
    Settings _settings;

    void Awake ()
    {
        _settings = FindObjectOfType<Settings> ();
        if (_spawnerPrefab)
        {
            _currentSpawner = Instantiate<TheLightSpawner> (_spawnerPrefab, transform.position, Quaternion.identity);
        }
    }

    void Start ()
    {
        if (_currentSpawner)
        {
            transform.position = new Vector3 (transform.position.x, _defaultStartY, transform.position.z);
            _currentSpawner.StartCoroutine ("Spawn");
        }

    }

    public void AddStar (TheStar theStar)
    {
        if (!_currentSpawner) return;
        _currentSpawner.AddStar (theStar);
    }

    public void SelfDestructTheStars ()
    {
        if (!_currentSpawner) return;
        _currentSpawner.StopCoroutine ("Spawn");
        _currentSpawner.SelfDestructTheStars ();
    }
}
