using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TheDaystarAppear : MonoBehaviour
{
    [SerializeField]
    float _appearPointY = 3.64f;
    [SerializeField]
    float _appearTime = 3f;
    [SerializeField]
    Transform _daystar;
    [SerializeField]
    string _defaultScene;
    Earthquake _earthquake;

    void Awake ()
    {
        _earthquake = FindObjectOfType<Earthquake> ();
    }

    void Start ()
    {
        StartCoroutine (Appear ());
    }

    IEnumerator Appear ()
    {
        _earthquake.StartEarthquake (true);
        var t = 0f;
        var srcPos = _daystar.position;
        var destPos = new Vector3 (srcPos.x, _appearPointY, srcPos.z);
        while (t <= 1f)
        {
            t += Time.deltaTime / _appearTime;
            _daystar.position = Vector3.Lerp (srcPos, destPos, t);
            yield return null;
        }
        yield return new WaitForSeconds (.75f);
        _earthquake.StopEarthquake ();
        SceneManager.LoadScene (string.Format ("Scenes/{0}", _defaultScene));
    }
}
