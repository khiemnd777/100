using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TheHouse : MonoBehaviour
{
    public Transform goOutPoint;
    public Transform theShepherdPoint;
    public int sheep = 1;
    public Transform initSheepPoint;
    public const int maxSheep = 100;
    [SerializeField]
    TextMesh _100txt;
    [SerializeField]
    string _nextScene;

    Settings _settings;
    TheHellFire _hellFire;
    TheLight _theLight;

    void Awake ()
    {
        _settings = FindObjectOfType<Settings> ();
        _hellFire = FindObjectOfType<TheHellFire> ();
        _theLight = FindObjectOfType<TheLight> ();
    }

    void Update ()
    {
        _100txt.text = sheep.ToString ();
    }

    public void OnConverted ()
    {
        sheep = sheep >= maxSheep ? maxSheep : sheep + 1;
        CollectEnough (sheep);
    }

    public void OnInfected ()
    {
        sheep = sheep <= 0 ? 0 : sheep - 1;
        // if (sheep <= 0)
        // {
        //     _settings.GameOver ();
        // }
    }

    public void OnHealed ()
    {
        sheep = sheep >= maxSheep ? maxSheep : sheep + 1;
        CollectEnough (sheep);
    }

    void CollectEnough (int sheep)
    {
        if (sheep < 100) return;
        _hellFire.GoToStartPoint (.5f);
        _theLight.SelfDestructTheStars ();
        StartCoroutine (GotoNextScene (_nextScene));
    }

    IEnumerator GotoNextScene (string scene)
    {
        if (string.IsNullOrEmpty (scene)) yield break;
        yield return new WaitForSeconds (.5f);
        SceneManager.LoadScene (string.Format ("Scenes/{0}", scene));
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.tag == "The Sheep")
        {
            var theSheep = other.GetComponent<TheSheep> ();
            if (theSheep.target.tag == "The House")
            {
                ++sheep;
                theSheep.SelfDestruct ();
            }
        }
    }
}
