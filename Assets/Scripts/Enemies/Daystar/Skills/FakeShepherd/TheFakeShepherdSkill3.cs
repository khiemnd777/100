using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheFakeShepherdSkill3 : MonoBehaviour
{
    public float initSpeed;
    public int number;
    [SerializeField]
    float _delay;
    [SerializeField]
    TheFakeShepherd3 _fakeShepherdPrefab;
    [SerializeField]
    Transform _spawnPoint;
    Settings _settings;
    int[] _initMaskList = new [] { 1, 1, 1, 1, 1 };

    void Awake ()
    {
        _settings = FindObjectOfType<Settings> ();
    }

    void Start ()
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
        while (number-- > 0)
        {
            var maskList = ComputeMaskList (_initMaskList);
            MapMaskToFakeShepherd (maskList);
            yield return new WaitForSeconds (_delay);
        }
    }

    void InitFakeShepherd (float stepX)
    {
        var spawnPointX = _spawnPoint.position.x + stepX;
        var spawnPoint = new Vector3 (spawnPointX, _spawnPoint.position.y, _spawnPoint.position.z);
        var theFakeShepherd = Instantiate<TheFakeShepherd3> (_fakeShepherdPrefab, spawnPoint, Quaternion.identity);
        theFakeShepherd.initSpeed = initSpeed;
    }
}
