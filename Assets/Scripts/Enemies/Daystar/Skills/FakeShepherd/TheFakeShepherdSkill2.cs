using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheFakeShepherdSkill2 : DaystarSkill
{
    public float initSpeed;
    public float acceleration;
    public float accelerationWaitedTime = .5f;
    public int number;
    [SerializeField]
    float _delay;
    [SerializeField]
    TheFakeShepherd _fakeShepherdPrefab;
    [SerializeField]
    Transform _spawnPoint;
    TheTraitorDueCount _theTraitorDueCount;
    TheDaystar _theDaystar;
    Settings _settings;
    int[] _initMaskList = new [] { 1, 1, 1, 1, 1 };
    int _number;

    void Awake ()
    {
        _theDaystar = FindObjectOfType<TheDaystar> ();
        _settings = FindObjectOfType<Settings> ();
        _theTraitorDueCount = GetComponent<TheTraitorDueCount> ();
    }

    public override void Execute ()
    {
        StartCoroutine (Spawn ());
    }

    int[] ComputeMaskList (int[] initMaskList)
    {
        var copiedList = new int[5];
        initMaskList.CopyTo (copiedList, 0);
        var voidIndex = Random.Range (0, copiedList.Length);
        copiedList[voidIndex] = 0;
        return copiedList;
    }

    void MapMaskToFakeShepherd (int[] maskList)
    {
        for (var i = 0; i < maskList.Length; i++)
        {
            var mask = maskList[i];
            if (mask == 0) continue;
            var stepX = _settings.GetStepXByIndex (_settings.specificHorizontalSteps, i);
            InitFakeShepherd (stepX);
        }
    }

    IEnumerator Spawn ()
    {
        _number = number;
        while (_number-- > 0)
        {
            var maskList = ComputeMaskList (_initMaskList);
            MapMaskToFakeShepherd (maskList);
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
