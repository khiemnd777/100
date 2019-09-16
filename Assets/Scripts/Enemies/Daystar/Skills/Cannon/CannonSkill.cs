using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonSkill : MonoBehaviour
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

    void Awake ()
    {
        _theShepherd = FindObjectOfType<TheShepherd> ();
    }

    void Start ()
    {
        StartCoroutine (Launch ());
    }

    IEnumerator Launch ()
    {
        // normal launch
        while (number-- > 0)
        {
            var forward = _theShepherd.transform.position - transform.position;
            forward.Normalize ();
            InstantiateBall (forward);
            yield return new WaitForSeconds (_delay);
        }
        // final launch
        var finalForward = _theShepherd.transform.position - transform.position;
        while (_finalNumber-- > 0)
        {
            var finalTargetForward = new Vector3 (finalForward.x + Random.Range (-.65f, .65f), finalForward.y, finalForward.z);
            finalTargetForward.Normalize ();
            InstantiateBall (finalTargetForward);
            yield return new WaitForSeconds (.125f);
        }
    }

    void InstantiateBall (Vector3 forward)
    {
        var cannonBall = Instantiate<CannonBall> (_theCannonBallPrefab, transform.position, Quaternion.identity);
        cannonBall.speed = _speed;
        cannonBall.forward = forward;
    }
}
