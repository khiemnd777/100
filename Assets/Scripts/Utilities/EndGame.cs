using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer _theEndBackgroundPrefab;
    [SerializeField]
    SpriteRenderer _addOilPrefab;

    public IEnumerator Play ()
    {
        yield return StartCoroutine (EndGameScript ());
    }

    IEnumerator EndGameScript ()
    {
        var theEndBg = Instantiate<SpriteRenderer> (_theEndBackgroundPrefab, Vector3.zero, Quaternion.identity);
        yield return new WaitForSeconds (1.5f);
        var t = 0f;
        var ca = new Color32 (255, 255, 255, 0);
        var cb = new Color32 (255, 255, 255, 255);
        while (t <= 1f)
        {
            t += Time.deltaTime / 2f;
            theEndBg.color = Color32.Lerp (ca, cb, t);
            yield return null;
        }
        t = 0f;
        var addOil = Instantiate<SpriteRenderer> (_addOilPrefab, new Vector3 (0f, .9f, 0f), Quaternion.identity);
        while (t <= 1f)
        {
            t += Time.deltaTime / 1.5f;
            addOil.color = Color32.Lerp (ca, cb, t);
            yield return null;
        }
        yield return new WaitForSeconds (3f);
        t = 0f;
        while (t <= 1f)
        {
            t += Time.deltaTime / 1.5f;
            addOil.color = Color32.Lerp (cb, ca, t);
            yield return null;
        }
        yield return new WaitForSeconds (1.25f);
        SceneManager.LoadScene (string.Format ("Scenes/{0}", "Tap to play"));
    }
}
