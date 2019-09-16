using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheLightSpawnerLevel1 : TheLightSpawner
{
    public float delay;
    [Header("Fallen Star")]
    [SerializeField]
    float _fallenStarHp;
    [SerializeField]
    float _fallenStarSpeed;
    [SerializeField]
    float _fallenInfectedStarSpeed;
    [Space]
    [SerializeField]
    Transform _spawnPoint;
    [SerializeField]
    YellowFallenStar _yellowFallenStarPrefab;
    Settings _settings;

    void Awake ()
    {
        _settings = FindObjectOfType<Settings> ();
    }

    public override IEnumerator Spawn ()
    {
        while (!_settings.gameOver)
        {
            var specificHorizontalStep = _settings.GetSpecificHorizontalStep (_settings.specificHorizontalSteps);
            var spawnPointX = _spawnPoint.position.x + specificHorizontalStep;
            var spawnPoint = new Vector3 (spawnPointX, _spawnPoint.position.y, _spawnPoint.position.z);
            var theStar = Instantiate<YellowFallenStar> (_yellowFallenStarPrefab, spawnPoint, Quaternion.identity);
            theStar.hp = theStar.maxHp = _fallenStarHp;
            theStar.speed = _fallenStarSpeed;
            theStar.infectedSpeed = _fallenInfectedStarSpeed;
            AddStar (theStar);
            yield return new WaitForSeconds (delay);
        }
    }
}
