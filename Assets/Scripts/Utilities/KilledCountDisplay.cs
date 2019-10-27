using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KilledCountDisplay : MonoBehaviour
{
    [SerializeField]
    SettingData _settingData;
    [SerializeField]
    TextMesh _text;
    [SerializeField]
    TextMesh _horizontalLine;
    [SerializeField]
    TextMesh _per;

    void Start ()
    {
        StartCoroutine (StandOut ());
        var isActive = _settingData.killedCount < 7000;
        _horizontalLine.gameObject.SetActive (isActive);
        _per.gameObject.SetActive (isActive);
    }

    void Update ()
    {
        _text.text = _settingData.killedCount.ToString ();
    }

    IEnumerator StandOut ()
    {
        while (_settingData.killedCount < 7000)
        {
            yield return null;
        }
        yield return null;
        StartCoroutine (Disappear ());
        var t = 0f;
        while (t <= 1f)
        {
            t += Time.deltaTime / .25f;
            _text.transform.localScale = Vector3.Lerp (Vector3.one, Vector3.one * 1.5f, t);
            yield return null;
        }
        t = 0f;
        while (t <= 1f)
        {
            t += Time.deltaTime / .25f;
            _text.transform.localScale = Vector3.Lerp (Vector3.one * 1.5f, Vector3.one, t);
            yield return null;
        }
    }

    IEnumerator Disappear ()
    {
        var t = 0f;
        while (t <= 1f)
        {
            t += Time.deltaTime / .5f;
            _horizontalLine.color = Color32.Lerp (new Color32 (0, 0, 0, 255), new Color32 (0, 0, 0, 0), t);
            _per.color = Color32.Lerp (new Color32 (0, 0, 0, 255), new Color32 (0, 0, 0, 0), t);
            yield return null;
        }
    }
}
