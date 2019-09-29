using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheTrueDaystar : MonoBehaviour
{
    [SerializeField]
    BubbleLight _bubbleLightPrefab;
    [SerializeField]
    float _maxSpeed;
    [SerializeField]
    SpriteRenderer _blastLightDisplay;
    TheTrueDaystarSkill _skill;
    TheTrueDaystarComeback _comeback;
    Earthquake _earthquake;

    void Awake ()
    {
        _skill = GetComponent<TheTrueDaystarSkill> ();
        _comeback = GetComponent<TheTrueDaystarComeback> ();
        _earthquake = FindObjectOfType<Earthquake> ();
    }

    void Start ()
    {
        StartCoroutine ("BubbleLight");
        StartCoroutine ("Twinkle");
    }

    IEnumerator BubbleLight ()
    {
        while (gameObject)
        {
            CreateBubbleLight ();
            yield return null;
        }
    }

    IEnumerator BubbleLightInverse ()
    {
        while (gameObject)
        {
            CreateBubbleLightInverse ();
            yield return null;
        }
    }

    void CreateBubbleLight ()
    {
        var pos = Random.insideUnitSphere * .5f + transform.position;
        var ins = Instantiate<BubbleLight> (_bubbleLightPrefab, pos, Quaternion.identity);
        ins.destructTime = 2.75f;
        ins.transform.localScale = Vector3.one * Random.Range (.5f, 1f);
        var forward = ins.transform.position - transform.position;
        forward.Normalize ();
        ins.speed = Random.Range (_maxSpeed / 3f, _maxSpeed);
        ins.direction = -1;
    }

    void CreateBubbleLightInverse ()
    {
        var pos = Random.insideUnitSphere * 12f + transform.position;
        var ins = Instantiate<BubbleLight> (_bubbleLightPrefab, pos, Quaternion.identity);
        ins.destructTime = .5f;
        ins.transform.localScale = Vector3.one * Random.Range (.5f, 1f);
        var forward = transform.position - ins.transform.position;
        forward.Normalize ();
        ins.speed = Random.Range (10f, 12f);
        ins.direction = 1;
    }

    IEnumerator Twinkle ()
    {
        var scale = _blastLightDisplay.transform.localScale.x;
        while (gameObject)
        {
            _blastLightDisplay.transform.localScale = Vector3.one * Random.Range (scale / 2f, scale);
            yield return null;
        }
    }

    void GameOver ()
    {
        StopCoroutine ("BubbleLight");
        StartCoroutine (TransformToBlack ());
    }

    IEnumerator TransformToBlack ()
    {
        StartCoroutine ("BubbleLightInverse");
        yield return new WaitForSeconds (5f);
        StopCoroutine ("Twinkle");
        StopCoroutine ("BubbleLightInverse");
        yield return StartCoroutine (_comeback.Comeback ());
        _earthquake.StopEarthquake ();
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.tag == "The Sheep" || other.tag == "The Infected Shepherd")
        {
            _skill.ConsumeSheep ();
            other.GetComponent<TheSheep> ().SelfDestruct ();
            if (_skill.isOutOfSheep)
            {
                GameOver ();
            }
        }
    }
}
