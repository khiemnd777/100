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
    Settings _settings;

    void Awake ()
    {
        _settings = FindObjectOfType<Settings> ();
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
        var spawnPointX = _spawnPoint.position.x + stepX;
        var spawnPoint = new Vector3 (spawnPointX, _spawnPoint.position.y, _spawnPoint.position.z);
        var theFakeShepherd = Instantiate<TheFakeShepherd> (_fakeShepherdPrefab, spawnPoint, Quaternion.identity);
        theFakeShepherd.initSpeed = initSpeed;
        theFakeShepherd.acceleration = acceleration;
        theFakeShepherd.accelerationWaitedTime = accelerationWaitedTime;
    }
}
