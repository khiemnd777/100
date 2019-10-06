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
    SpriteRenderer _twinkleDisplay;
    [SerializeField]
    Transform _blastLight;
    [SerializeField]
    SpriteRenderer _blastLightDisplay;
    [SerializeField]
    SpriteRenderer _display;
    TheTrueDaystarSkill _skill;
    TheTrueDaystarGoesAway _goesAway;
    TheTrueDaystarComeback _comeback;
    TheHellFire _theHellFire;
    Earthquake _earthquake;

    void Awake ()
    {
        _skill = GetComponent<TheTrueDaystarSkill> ();
        _comeback = GetComponent<TheTrueDaystarComeback> ();
        _goesAway = GetComponent<TheTrueDaystarGoesAway> ();
        _earthquake = FindObjectOfType<Earthquake> ();
        _theHellFire = FindObjectOfType<TheHellFire> ();
        _blastLight.localScale = Vector3.zero;
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
        var scale = _twinkleDisplay.transform.localScale.x;
        while (gameObject)
        {
            _twinkleDisplay.transform.localScale = Vector3.one * Random.Range (scale / 2f, scale);
            yield return null;
        }
    }

    void GameOver ()
    {
        StopCoroutine ("BubbleLight");
        StartCoroutine (TransformToBlack ());
    }

    void ReleaseAndDecay ()
    {
        StopCoroutine ("BubbleLight");
        StartCoroutine (ScriptReleaseAndDecay ());
    }

    IEnumerator ScriptReleaseAndDecay ()
    {
        yield return StartCoroutine (_skill.SheepGoingHome2 ());
        yield return StartCoroutine (_skill.SheepGoingHome ());
        yield return StartCoroutine (BlastLight ());
        StartCoroutine (TheHellFireIsGone ());
        yield return new WaitForSeconds (1.25f);
        _display.color = new Color32 (255, 255, 255, 0);
        yield return StartCoroutine (DissolveBlastLight ());
        Destroy (gameObject);
    }

    IEnumerator TheHellFireIsGone ()
    {
        var t = 0f;
        var srcPos = _theHellFire.transform.position;
        var destPos = new Vector3 (srcPos.x, 14f, srcPos.z);
        while (t <= 1f)
        {
            t += Time.deltaTime * 5f;
            _theHellFire.transform.position = Vector3.Lerp (srcPos, destPos, t);
            yield return null;
        }
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

    IEnumerator BlastLight ()
    {
        var t = 0f;
        while (t <= 1f)
        {
            t += Time.deltaTime / 2f;
            _blastLight.localScale = Vector3.Lerp (Vector3.zero, Vector3.one * 100, t);
            yield return null;
        }
    }

    IEnumerator DissolveBlastLight ()
    {
        var t = 0f;
        while (t <= 1f)
        {
            t += Time.deltaTime / 1f;
            _blastLightDisplay.color = Color32.Lerp (new Color32 (255, 255, 255, 255), new Color32 (255, 255, 255, 0), t);
            yield return null;
        }
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.tag == "The Sheep" || other.tag == "The Infected Shepherd")
        {
            var sheep = other.GetComponent<TheSheep> ();
            if (sheep.target.GetInstanceID () == transform.GetInstanceID ())
            {
                _skill.ConsumeSheep (sheep);
                other.GetComponent<TheSheep> ().SelfDestruct ();
                if (_skill.isOutOfSheep)
                {
                    GameOver ();
                }
            }
        }
        else if (other.tag == "Daystar Death Point")
        {
            _goesAway.Stop ();
            ReleaseAndDecay ();
        }
    }
}
