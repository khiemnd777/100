using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TheFallenWallSkill : DaystarSkill
{
    public int number;
    public float initSpeed;
    [SerializeField]
    float _delay;
    [SerializeField]
    TheGoingDown _theFallenBlockPrefab;
    [SerializeField]
    Transform _spawnPoint;
    Settings _settings;
    int[] _initMaskList = new [] { 1, 1, 1, 1, 1 };
    List<TheGoingDown> _currentList = new List<TheGoingDown> ();
    int _number;
    void Awake ()
    {
        _settings = FindObjectOfType<Settings> ();
    }

    public override void Execute ()
    {
        StartCoroutine (Spawn ());
    }

    IEnumerator Spawn ()
    {
        _number = number;
        while (number-- > 0)
        {
            _currentList.Clear ();
            var maskList = ComputeMaskList (_initMaskList);
            MapMaskToFakeShepherd (maskList);
            yield return StartCoroutine (Appear ());
            yield return new WaitForSeconds (_delay);
        }
        if (onEnd != null)
        {
            onEnd ();
        }
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
            InitGoingDownThing (stepX);
        }
    }

    IEnumerator Appear ()
    {
        if (!_currentList.Any ()) yield break;
        var t = 0f;
        while (t <= 1f)
        {
            t += Time.deltaTime * 20f;
            foreach (var o in _currentList)
            {
                o.transform.localScale = Vector3.Lerp (Vector3.zero, Vector3.one, t);
            }
            yield return null;
        }
    }

    void InitGoingDownThing (float stepX)
    {
        var spawnPointX = _spawnPoint.position.x + stepX;
        var spawnPoint = new Vector3 (spawnPointX, _spawnPoint.position.y, _spawnPoint.position.z);
        var goingDown = Instantiate<TheGoingDown> (_theFallenBlockPrefab, spawnPoint, Quaternion.identity);
        goingDown.initSpeed = initSpeed;
        goingDown.transform.localScale = Vector3.zero;
        _currentList.Add (goingDown);
    }
}
