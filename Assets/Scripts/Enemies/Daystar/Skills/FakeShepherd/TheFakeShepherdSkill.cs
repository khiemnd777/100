using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheFakeShepherdSkill : DaystarSkill
{
    public float initSpeed;
    public float acceleration;
    public float accelerationWaitedTime = .5f;
    [SerializeField]
    float _delay;
    [SerializeField]
    TheFakeShepherd _fakeShepherdPrefab;
    [SerializeField]
    Transform _spawnPoint;
    TheTraitorDueCount _theTraitorDueCount;
    TheDaystar _theDaystar;
    Settings _settings;

    void Awake ()
    {
        _theDaystar = FindObjectOfType<TheDaystar> ();
        _settings = FindObjectOfType<Settings> ();
        _theTraitorDueCount = GetComponent<TheTraitorDueCount> ();
    }

    public override void Execute ()
    {
        if (Random.Range (0, 2) == 0)
        {
            StartCoroutine (Spawn1 ());
        }
        else
        {
            StartCoroutine (Spawn2 ());
        }
    }

    public IEnumerator Spawn1 ()
    {
        for (var i = 0; i < _settings.specificHorizontalSteps.Length; i++)
        {
            var stepX = _settings.specificHorizontalSteps[i];
            InitFakeShepherd (stepX);
            if (i < _settings.specificHorizontalSteps.Length)
            {
                InitFakeShepherd (-stepX);
            }
            yield return new WaitForSeconds (_delay);
        }
        for (var i = _settings.specificHorizontalSteps.Length - 2; i >= 0; i--)
        {
            var stepX = _settings.specificHorizontalSteps[i];
            InitFakeShepherd (stepX);
            if (i >= 0)
            {
                InitFakeShepherd (-stepX);
            }
            yield return new WaitForSeconds (_delay);
        }
        for (var i = 0; i < _settings.specificHorizontalSteps.Length; i++)
        {
            var stepX = _settings.specificHorizontalSteps[i];
            InitFakeShepherd (stepX);
            if (i < _settings.specificHorizontalSteps.Length)
            {
                InitFakeShepherd (-stepX);
            }
            yield return new WaitForSeconds (_delay);
        }
        if (onEnd != null)
        {
            onEnd ();
        }
    }

    public IEnumerator Spawn2 ()
    {
        for (var i = _settings.specificHorizontalSteps.Length - 1; i >= 0; i--)
        {
            var stepX = _settings.specificHorizontalSteps[i];
            InitFakeShepherd (stepX);
            if (i >= 0)
            {
                InitFakeShepherd (-stepX);
            }
            yield return new WaitForSeconds (_delay);
        }
        for (var i = 1; i < _settings.specificHorizontalSteps.Length; i++)
        {
            var stepX = _settings.specificHorizontalSteps[i];
            InitFakeShepherd (stepX);
            if (i < _settings.specificHorizontalSteps.Length)
            {
                InitFakeShepherd (-stepX);
            }
            yield return new WaitForSeconds (_delay);
        }
        for (var i = _settings.specificHorizontalSteps.Length - 1; i >= 0; i--)
        {
            var stepX = _settings.specificHorizontalSteps[i];
            InitFakeShepherd (stepX);
            if (i >= 0)
            {
                InitFakeShepherd (-stepX);
            }
            yield return new WaitForSeconds (_delay);
        }
        if (onEnd != null)
        {
            onEnd ();
        }
    }

    void InitFakeShepherd (float stepX)
    {
        var normalizedHp = _theDaystar.GetNormalizeHp ();
        var speed = normalizedHp <= (1f / 7f) ? initSpeed * 1.5f : initSpeed;
        var accel = normalizedHp <= (1f / 7f) ? acceleration * 1.5f : acceleration;
        var spawnPointX = _spawnPoint.position.x + stepX;
        var spawnPoint = new Vector3 (spawnPointX, _spawnPoint.position.y, _spawnPoint.position.z);
        var theFakeShepherd = Instantiate<TheFakeShepherd> (_fakeShepherdPrefab, spawnPoint, Quaternion.identity);
        theFakeShepherd.theTraitor.isTraitor = _theTraitorDueCount.isDue;
        theFakeShepherd.initSpeed = speed;
        theFakeShepherd.acceleration = accel;
        theFakeShepherd.accelerationWaitedTime = accelerationWaitedTime;
    }
}
