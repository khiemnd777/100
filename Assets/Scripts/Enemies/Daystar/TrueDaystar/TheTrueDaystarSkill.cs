using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheTrueDaystarSkill : MonoBehaviour
{
    [SerializeField]
    TheSheep _theSheepPrefab;
    [SerializeField]
    TheSheep _theInfectedShepherdPrefab;
    [SerializeField]
    float _theSheepSpeed;
    TheHouse _theHouse;
    TheShepherd _theShepherd;
    [SerializeField]
    int _outSheep;
    int _consumedSheep;
    int _maxSheep;
    List<TheSheep> _insSheepList = new List<TheSheep> ();

    public bool isOutOfSheep
    {
        get
        {
            return _consumedSheep >= _maxSheep;
        }
    }

    public bool isOutOfComsumedSheep
    {
        get
        {
            return _consumedSheep <= 0;
        }
    }

    void Awake ()
    {
        _theShepherd = FindObjectOfType<TheShepherd> ();
        _theHouse = FindObjectOfType<TheHouse> ();
    }

    void Start ()
    {
        _maxSheep = _theHouse.sheep - (_theHouse.sheep == 1 ? 0 : 1);
        StartCoroutine (SheepFlyOut ());
    }

    IEnumerator SheepFlyOut_bak ()
    {
        while (_theHouse.sheep > 0)
        {
            ++_outSheep;
            _theHouse.OnInfected ();
            if (_theHouse.sheep == 0)
            {
                var ins = Instantiate<TheSheep> (_theInfectedShepherdPrefab, _theShepherd.transform.position, Quaternion.identity);
                var forward = transform.position - ins.transform.position;
                forward.Normalize ();
                ins.speed = _theSheepSpeed / 2.5f;
                _theShepherd.gameObject.SetActive (false);
            }
            else
            {
                var pos = _theHouse.transform.position;
                pos.x = Random.Range (-.8f, .8f);
                var ins = Instantiate<TheSheep> (_theSheepPrefab, pos, Quaternion.identity);
                ins.transform.localScale = Vector3.one * Random.Range (.7f, 1f);
                var forward = transform.position - ins.transform.position;
                forward.Normalize ();
                ins.speed = Random.Range (_theSheepSpeed / 2.5f, _theSheepSpeed);
            }
            yield return new WaitForSeconds (.085f);
        }
    }

    IEnumerator SheepFlyOut ()
    {
        if (_theHouse.sheep == 1)
        {
            _theHouse.OnInfected ();
            var ins = Instantiate<TheSheep> (_theInfectedShepherdPrefab, _theShepherd.transform.position, Quaternion.identity);
            ins.target = this.transform;
            var forward = transform.position - ins.transform.position;
            forward.Normalize ();
            ins.speed = _theSheepSpeed / 2.5f;
            _theShepherd.gameObject.SetActive (false);
            yield break;
        }
        while (_theHouse.sheep > 1)
        {
            _theHouse.OnInfected ();
            var pos = _theHouse.initSheepPoint.position;
            pos.x = Random.Range (-.8f, .8f);
            var ins = Instantiate<TheSheep> (_theSheepPrefab, pos, Quaternion.identity);
            ins.target = this.transform;
            ins.transform.localScale = Vector3.one * Random.Range (.7f, 1f);
            ins.speed = Random.Range (_theSheepSpeed / 2.5f, _theSheepSpeed);
            _insSheepList.Add (ins);
            yield return new WaitForSeconds (.085f);
        }
    }

    public IEnumerator SheepGoingHome ()
    {
        while (_consumedSheep-- > 0)
        {
            var ins = Instantiate<TheSheep> (_theSheepPrefab, transform.position, Quaternion.identity);
            ins.target = _theHouse.transform;
            ins.transform.localScale = Vector3.one * Random.Range (.7f, 1f);
            ins.speed = Random.Range (_theSheepSpeed / 2.5f, _theSheepSpeed);
            yield return new WaitForSeconds (.02f);
        }
    }

    public IEnumerator SheepGoingHome2 ()
    {
        foreach (var sheep in _insSheepList)
        {
            if (!sheep) continue;
            sheep.target = _theHouse.transform;
            yield return new WaitForSeconds (.02f);
        }
    }

    public void ConsumeSheep (TheSheep theSheep)
    {
        ++_consumedSheep;
        _insSheepList.Remove (theSheep);
    }
}
