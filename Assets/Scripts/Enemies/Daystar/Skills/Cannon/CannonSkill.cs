using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonSkill : DaystarSkill
{
    public int number;
    [SerializeField]
    int _finalNumber;
    [SerializeField]
    float _speed;
    [SerializeField]
    CannonBall _theCannonBallPrefab;
    [SerializeField]
    float _delay;
    [SerializeField]
    Transform _projectilePoint;
    TheShepherd _theShepherd;
    int _number;
    int _numberOfFinal;

    void Awake ()
    {
        _theShepherd = FindObjectOfType<TheShepherd> ();
    }

    public override void Execute ()
    {
        StartCoroutine (Launch ());
    }

    IEnumerator Launch ()
    {
        _number = number;
        // normal launch
        while (_number-- > 0)
        {
            var forward = _theShepherd.transform.position - transform.position;
            forward.Normalize ();
            InstantiateBall (forward);
            yield return new WaitForSeconds (_delay);
        }
        // final launch
        _numberOfFinal = _finalNumber;
        var finalForward = _theShepherd.transform.position - transform.position;
        while (_numberOfFinal-- > 0)
        {
            var finalTargetForward = new Vector3 (finalForward.x + Random.Range (-.65f, .65f), finalForward.y, finalForward.z);
            finalTargetForward.Normalize ();
            InstantiateBall (finalTargetForward);
            yield return new WaitForSeconds (.125f);
        }
        if (onEnd != null)
        {
            onEnd();
        }
    }

    void InstantiateBall (Vector3 forward)
    {
        var cannonBall = Instantiate<CannonBall> (_theCannonBallPrefab, transform.position, Quaternion.identity);
        cannonBall.speed = _speed;
        cannonBall.forward = forward;
    }
}
