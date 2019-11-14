using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixSettingData : MonoBehaviour
{
    [SerializeField]
    SettingData _settingData;

    void Start ()
    {
        _settingData.videoAdsCooldown = 3;
    }
}
