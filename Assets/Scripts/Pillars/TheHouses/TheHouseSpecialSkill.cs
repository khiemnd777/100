using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TheHouseSpecialSkill : MonoBehaviour
{
    [SerializeField]
    Button _execBtn;
    [SerializeField]
    TheHouseEnergyRadioactive _radioactivePrefab;
    TheHouse _theHouse;
    CameraShake _cameraShake;

    void Awake ()
    {
        _execBtn.onClick.AddListener (Execute);
        _theHouse = GetComponent<TheHouse> ();
        _cameraShake = FindObjectOfType<CameraShake> ();
    }

    void Execute ()
    {
        if (_theHouse.sheep > 50)
        {
            Instantiate (_radioactivePrefab, transform.position, Quaternion.identity);
            _theHouse.sheep /= 2;
            _cameraShake.Shake (.3f, .095f, true);
        }
    }
}
