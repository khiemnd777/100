using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheFallenBlockSkill : DaystarSkill
{
    public float initSpeed;
    public float appearedSpeed;
    public int number;
    [SerializeField]
    float _delay;
    [SerializeField]
    TheFakeShepherd3 _fakeShepherdPrefab;
    [SerializeField]
    Transform _spawnPoint;
    TheTraitorDueCount _theTraitorDueCount;
    Settings _settings;
    TheShepherd _player;
    int _number;

    void Awake ()
    {
        _settings = FindObjectOfType<Settings> ();
        _player = FindObjectOfType<TheShepherd> ();
        _theTraitorDueCount = GetComponent<TheTraitorDueCount> ();
    }

    public override void Execute ()
    {
        StartCoroutine (Spawn ());
    }

    IEnumerator Spawn ()
    {
        _number = number;
        while (_number-- > 0)
        {
            InitFakeShepherd ();
            yield return new WaitForSeconds (_delay);
        }
        if (onEnd != null)
        {
            onEnd ();
        }
    }

    void InitFakeShepherd ()
    {
        var spawnPoint = new Vector3 (_player.transform.position.x, _spawnPoint.position.y, _spawnPoint.position.z);
        var theFakeShepherd = Instantiate<TheFakeShepherd3> (_fakeShepherdPrefab, spawnPoint, Quaternion.identity);
        theFakeShepherd.theTraitor.isTraitor = _theTraitorDueCount.isDue;
        theFakeShepherd.initSpeed = initSpeed;
        theFakeShepherd.appearedSpeed = appearedSpeed;
    }
}
