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
    [SerializeField]
    SpriteRenderer _blackBg;
    [SerializeField]
    AudioSource _openedSoundTrack;
    Earthquake _earthquake;
    Settings _settings;

    void Awake ()
    {
        _settings = FindObjectOfType<Settings> ();
        _earthquake = FindObjectOfType<Earthquake> ();
    }

    void Start ()
    {
        StartCoroutine (Scripting ());
    }

    IEnumerator Scripting ()
    {
        yield return StartCoroutine (VolumePreloadingSoundTracksGoingDown ());
        yield return new WaitForSecondsRealtime (.625f);
        _openedSoundTrack.Play ();
        StartCoroutine (BlackBackgroundDisappear ());
        StartCoroutine (Appear ());
    }

    IEnumerator BlackBackgroundDisappear ()
    {
        var t = 0f;
        var ca = new Color32 (255, 255, 255, 255);
        var cb = new Color32 (255, 255, 255, 0);
        while (t <= 1f)
        {
            t += Time.deltaTime / .5f;
            _blackBg.color = Color32.Lerp (ca, cb, t);
            yield return null;
        }
    }

    IEnumerator VolumePreloadingSoundTracksGoingDown ()
    {
        var preloadingSoundTracks = GameObject.Find ("Playlist Manager On Preloading");
        if (!preloadingSoundTracks) yield break;
        var playlistManager = preloadingSoundTracks.GetComponent<PlaylistManager> ();
        var srcVol = playlistManager.volume;
        var t = 0f;
        while (t <= 1f)
        {
            t += Time.deltaTime / 1f;
            playlistManager.volume = Mathf.Lerp (srcVol, 0f, t);
            yield return null;
        }
        Destroy (preloadingSoundTracks);
    }

    IEnumerator Appear ()
    {
        _earthquake.StartEarthquake ();
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
