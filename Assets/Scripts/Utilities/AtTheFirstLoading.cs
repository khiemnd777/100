using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtTheFirstLoading : MonoBehaviour
{
    [SerializeField]
    SettingData _settingData;

    void Start ()
    {
        _settingData.slowmotion = false;
    }
}
