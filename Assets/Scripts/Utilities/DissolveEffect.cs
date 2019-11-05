using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveEffect : MonoBehaviour
{
    [SerializeField]
    float _dissolveTime;
    [SerializeField]
    SpriteRenderer _display;

    void Start ()
    {
        StartCoroutine (Dissolve ());
    }

    IEnumerator Dissolve ()
    {
        var t = 0f;
        while (t <= 1f)
        {
            t += Time.deltaTime / _dissolveTime;
            _display.color = Color32.Lerp (new Color32 (255, 255, 255, 255), new Color32 (255, 255, 255, 0), t);
            yield return null;
        }
        Destroy (gameObject);
    }
}
