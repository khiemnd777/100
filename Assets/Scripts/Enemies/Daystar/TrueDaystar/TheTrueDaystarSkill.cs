using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheTrueDaystarSkill : MonoBehaviour
{
    [SerializeField]
    TheSheep _theSheepPrefab;
    [SerializeField]
    float _theSheepSpeed;
    TheHouse _theHouse;
    public int outSheep;

    public bool isOutOfSheep
    {
        get
        {
            return outSheep <= 0;
        }
    }

    void Awake ()
    {
        _theHouse = FindObjectOfType<TheHouse> ();
    }

    void Start ()
    {
        StartCoroutine (SheepFlyOut ());
    }

    IEnumerator SheepFlyOut ()
    {
        while (_theHouse.sheep > 0)
        {
            ++outSheep;
            _theHouse.OnInfected ();
            var pos = _theHouse.transform.position;
            pos.x = Random.Range (-.8f, .8f);
            var ins = Instantiate<TheSheep> (_theSheepPrefab, pos, Quaternion.identity);
            ins.transform.localScale = Vector3.one * Random.Range (.7f, 1f);
            var forward = transform.position - ins.transform.position;
            forward.Normalize ();
            ins.speed = Random.Range (_theSheepSpeed / 2.5f, _theSheepSpeed);
            yield return new WaitForSeconds (.085f);
        }
    }

    public void ConsumeSheep ()
    {
        --outSheep;
    }
}
