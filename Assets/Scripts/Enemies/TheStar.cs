using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheStar : Enemy
{
    public int damage;
    public float speed;
    [SerializeField]
    protected SpriteRenderer display;
    [SerializeField]
    float _rotationSpeedModifier = 200f;
    [SerializeField]
    protected SettingData _settingData;
    float _rotationSpeed;
    int _rotationDirection;
    int[] _indicatedRotationDirections = {-1, 1 };

    public override void Awake ()
    {
        base.Awake ();
        _rotationDirection = _indicatedRotationDirections[Random.Range (0, _indicatedRotationDirections.Length)];
        _rotationSpeed = _rotationSpeedModifier * Random.Range (.75f, 1f);

    }

    public override void Start ()
    {
        base.Start ();
    }

    public override void Update ()
    {
        base.Update ();
        display.transform.RotateAround (display.transform.position, Vector3.forward * _rotationDirection, Time.deltaTime * _rotationSpeed);
    }

    public override void FixedUpdate ()
    {
        base.FixedUpdate ();
    }

    public virtual void SelfDestruct ()
    {

    }
}
