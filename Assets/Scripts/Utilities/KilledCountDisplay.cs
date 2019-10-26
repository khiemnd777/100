using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KilledCountDisplay : MonoBehaviour
{
    [SerializeField]
    SettingData _settingData;
    [SerializeField]
    TextMesh _text;

    void Update ()
    {
        _text.text = _settingData.killedCount.ToString ();
    }
}
