using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheLightSpawnerLevel3 : TheLightSpawner
{
    public float delay;
    public float[] probabilities;
    [Header ("Heavy Fallen Star")]
    [SerializeField]
    float _heavyFallenStarHp;
    [SerializeField]
    float _heavyFallenStarSpeed;
    [SerializeField]
    int _heavyFallenStarDamage;
    [SerializeField]
    float _heavyFallenInfectedStarSpeed;
    [SerializeField]
    HeavyFallenStar _heavyYellowFallenStarPrefab;

    [Header ("Fallen Star")]
    [SerializeField]
    float _fallenStarHp;
    [SerializeField]
    float _fallenStarSpeed;
    [SerializeField]
    int _fallenStarDamage;
    [SerializeField]
    float _fallenInfectedStarSpeed;
    [SerializeField]
    YellowFallenStar _yellowFallenStarPrefab;
    [Space]
    [SerializeField]
    Transform _spawnPoint;

    Settings _settings;
    int[] probSpawn;

    void Awake ()
    {
        _settings = FindObjectOfType<Settings> ();
    }

    void Start ()
    {
        probSpawn = Probability.Initialize (new [] { 0, 1 }, probabilities);
    }

    public override IEnumerator Spawn ()
    {
        while (!_settings.gameOver)
        {
            var prob = Probability.GetValueInProbability (probSpawn);
            if (prob == 0)
            {
                SpawnFallenStar ();
            }
            else
            {
                SpawnHeavyFallenStar ();
            }
            yield return new WaitForSeconds (delay);
        }
    }

    void SpawnHeavyFallenStar ()
    {
        var specificHorizontalStep = _settings.GetSpecificHorizontalStep (_settings.specificHorizontalSteps);
        var spawnPointX = _spawnPoint.position.x + specificHorizontalStep;
        var spawnPoint = new Vector3 (spawnPointX, _spawnPoint.position.y, _spawnPoint.position.z);
        var theStar = Instantiate<HeavyFallenStar> (_heavyYellowFallenStarPrefab, spawnPoint, Quaternion.identity);
        theStar.starNumber = 5;
        theStar.hp = theStar.maxHp = _heavyFallenStarHp;
        theStar.speed = _heavyFallenStarSpeed;
        theStar.damage = _heavyFallenStarDamage;
        theStar.infectedSpeed = _heavyFallenInfectedStarSpeed;
        theStar.yellowHp = _fallenStarHp;
        theStar.yellowSpeed = _fallenStarSpeed;
        theStar.yellowDamage = _fallenStarDamage;
        theStar.yellowInfectedSpeed = _fallenInfectedStarSpeed;
        AddStar (theStar);
    }

    void SpawnFallenStar ()
    {
        var specificHorizontalStep = _settings.GetSpecificHorizontalStep (_settings.specificHorizontalSteps);
        var spawnPointX = _spawnPoint.position.x + specificHorizontalStep;
        var spawnPoint = new Vector3 (spawnPointX, _spawnPoint.position.y, _spawnPoint.position.z);
        var theStar = Instantiate<YellowFallenStar> (_yellowFallenStarPrefab, spawnPoint, Quaternion.identity);
        theStar.hp = theStar.maxHp = _fallenStarHp;
        theStar.speed = _fallenStarSpeed;
        theStar.damage = _fallenStarDamage;
        theStar.infectedSpeed = _fallenInfectedStarSpeed;
        AddStar (theStar);
    }
}
