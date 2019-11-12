using UnityEngine;

public class PlayOstIfItDoesntExist : MonoBehaviour
{
    [SerializeField]
    PlaylistManager _ostPrefab;
    public float volumeMax;
    public float volumeInBattle;

    void Start ()
    {
        var preloadingSoundTracks = GameObject.Find ("Playlist Manager On Preloading");
        if (!preloadingSoundTracks)
        {
            var insOst = Instantiate<PlaylistManager> (_ostPrefab, Vector3.zero, Quaternion.identity);
            insOst.volume = volumeMax;
            insOst.name = "Playlist Manager On Preloading";
            insOst.playOnAwake = true;
            insOst.dontDestroyOnLoad = true;
        }
        else
        {
            var playlistManager = preloadingSoundTracks.GetComponent<PlaylistManager> ();
            playlistManager.volume = volumeMax;
        }
    }
}
