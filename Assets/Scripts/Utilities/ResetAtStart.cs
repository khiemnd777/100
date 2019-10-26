using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAtStart : MonoBehaviour
{
    [SerializeField]
    SettingData _settingData;

    void Start ()
    {
        _settingData.ResetKilledCount ();
    }
}
