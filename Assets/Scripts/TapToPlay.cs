using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TapToPlay : MonoBehaviour
{
    [SerializeField]
    float _speed;
    public Button tap2PlayButton;
    public Button againstDaystarButton;
    public Button againstDaystarDisabledButton;
    [SerializeField]
    Transform _logo;
    [SerializeField]
    Transform _theHellFire;
    [SerializeField]
    float _hellFireY;
    [Space]
    [SerializeField]
    Transform _theLight;
    [SerializeField]
    float _theLightY;
    [Space]
    [SerializeField]
    Transform _theHouse;
    [SerializeField]
    Transform _twitter;
    [SerializeField]
    Transform _facebook;
    [SerializeField]
    string _defaultScene;
    [SerializeField]
    PlayOstIfItDoesntExist _playOst;

    void Awake ()
    {
        tap2PlayButton.onClick.AddListener (OnTap);
    }

    void OnTap ()
    {
        tap2PlayButton.gameObject.SetActive (false);
        againstDaystarButton.gameObject.SetActive (false);
        againstDaystarDisabledButton.gameObject.SetActive (false);
        _logo.gameObject.SetActive (false);
        _twitter.gameObject.SetActive (false);
        _facebook.gameObject.SetActive (false);
        StartCoroutine (ScriptOnTap ());
        StartCoroutine (VolumePreloadingSoundTracksGoingDown ());
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
            t += Time.deltaTime * _speed;
            playlistManager.volume = Mathf.Lerp (srcVol, _playOst.volumeInBattle, t);
            yield return null;
        }
    }

    IEnumerator ScriptOnTap ()
    {
        var t = 0f;
        var srcTheLight = _theLight.position;
        var destTheLight = new Vector3 (srcTheLight.x, _theLightY - .5f, srcTheLight.z);
        var srcHellFire = _theHellFire.position;
        var destHellFire = new Vector3 (srcHellFire.x, _hellFireY + .5f, srcHellFire.z);
        while (t <= 1f)
        {
            t += Time.deltaTime * _speed;
            // The house
            _theHouse.localScale = Vector3.Lerp (Vector3.zero, Vector3.one * 1.15f, t);
            // The light
            _theLight.position = Vector3.Lerp (srcTheLight, destTheLight, t);
            // The hell fire
            _theHellFire.position = Vector3.Lerp (srcHellFire, destHellFire, t);
            yield return null;
        }
        t = 0f;
        srcTheLight = new Vector3 (srcTheLight.x, _theLightY - .5f, srcTheLight.z);
        destTheLight = new Vector3 (srcTheLight.x, _theLightY, srcTheLight.z);
        srcHellFire = new Vector3 (srcHellFire.x, _hellFireY + .5f, srcHellFire.z);
        destHellFire = new Vector3 (srcHellFire.x, _hellFireY, srcHellFire.z);
        while (t <= 1f)
        {
            t += Time.deltaTime * _speed;
            // The house
            _theHouse.localScale = Vector3.Lerp (Vector3.one * 1.15f, Vector3.one, t);
            // The light
            _theLight.position = Vector3.Lerp (srcTheLight, destTheLight, t);
            // The hell fire
            _theHellFire.position = Vector3.Lerp (srcHellFire, destHellFire, t);
            yield return null;
        }
        SceneManager.LoadScene (string.Format ("Scenes/{0}", _defaultScene));
    }
}
