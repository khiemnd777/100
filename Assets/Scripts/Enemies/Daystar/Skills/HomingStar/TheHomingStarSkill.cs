using System.Collections;
using UnityEngine;

public class TheHomingStarSkill : DaystarSkill
{
    public int number;
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
    TheTraitorDueCount _traitorDueCount;
    Settings _settings;
    int _number;

    void Awake ()
    {
        _settings = FindObjectOfType<Settings> ();
        _traitorDueCount = GetComponent<TheTraitorDueCount> ();
    }

    public override void Execute ()
    {
        StartCoroutine (Spawn ());
    }

    public IEnumerator Spawn ()
    {
        _number = number;
        while (_number-- > 0)
        {
            SpawnFallenStar ();
            yield return new WaitForSeconds (_delay);
        }
        if (onEnd != null)
        {
            onEnd ();
        }
    }

    void SpawnFallenStar ()
    {
        var specificHorizontalStep = _settings.GetSpecificHorizontalStep (_settings.specificHorizontalSteps);
        var spawnPointX = _spawnPoint.position.x + specificHorizontalStep;
        var spawnPoint = new Vector3 (spawnPointX, _spawnPoint.position.y, _spawnPoint.position.z);
        var theStar = Instantiate<TheHomingStar> (_homingStarPrefab, spawnPoint, Quaternion.identity);
        theStar.theTraitor.isTraitor = _traitorDueCount.isDue;
        theStar.speed = speed;
        theStar.maxDegreesDeltaRotation = maxDegreesDeltaRotation;
        theStar.acceleration = acceleration;
        theStar.maxDegreesDeltaRotationAcceleration = maxDegreesDeltaRotationAcceleration;
        theStar.accelerationWaitedTime = accelerationWaitedTime;
    }
}
