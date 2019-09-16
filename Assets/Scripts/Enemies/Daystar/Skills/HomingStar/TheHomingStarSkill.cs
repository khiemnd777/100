using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheHomingStarSkill : MonoBehaviour
{
    public float speed = 5f;
    public float maxDegreesDeltaRotation = 75f;
    public float acceleration = 7f;
    public float maxDegreesDeltaRotationAcceleration = 100f;
    public float accelerationWaitedTime = .5f;
    [SerializeField]
    float _delay;
    [SerializeField]
    TheHomingStar _homingStarPrefab;
    [SerializeField]
    Transform _spawnPoint;
    Settings _settings;

    void Awake ()
    {
        _settings = FindObjectOfType<Settings> ();
    }

    void Start ()
    {
        StartCoroutine (Spawn ());
    }

    public IEnumerator Spawn ()
    {
        while (!_settings.gameOver)
        {
            SpawnFallenStar ();
            yield return new WaitForSeconds (_delay);
        }
    }

    void SpawnFallenStar ()
    {
        var specificHorizontalStep = _settings.GetSpecificHorizontalStep (_settings.specificHorizontalSteps);
        var spawnPointX = _spawnPoint.position.x + specificHorizontalStep;
        var spawnPoint = new Vector3 (spawnPointX, _spawnPoint.position.y, _spawnPoint.position.z);
        var theStar = Instantiate<TheHomingStar> (_homingStarPrefab, spawnPoint, Quaternion.identity);
        theStar.speed = speed;
        theStar.maxDegreesDeltaRotation = maxDegreesDeltaRotation;
        theStar.acceleration = acceleration;
        theStar.maxDegreesDeltaRotationAcceleration = maxDegreesDeltaRotationAcceleration;
        theStar.accelerationWaitedTime = accelerationWaitedTime;
    }
}
