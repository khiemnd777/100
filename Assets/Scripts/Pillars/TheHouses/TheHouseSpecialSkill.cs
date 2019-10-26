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
    int _tapCount;
    float _tapTime;

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
            ++_tapCount;
            if (_tapCount >= 2)
            {
                if (Time.time - _tapTime <= .325f)
                {
                    Instantiate (_radioactivePrefab, transform.position, Quaternion.identity);
                    _theHouse.sheep /= 2;
                    _cameraShake.Shake (.3f, .095f, true);
                    _tapCount = 0;
                    _tapTime = 0f;
                }
            }
            _tapTime = Time.time;
        }
    }
}
