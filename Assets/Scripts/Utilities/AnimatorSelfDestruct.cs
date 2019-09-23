using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Animator))]
public class AnimatorSelfDestruct : MonoBehaviour
{
    Animator _animator;

    void Awake ()
    {
        _animator = GetComponent<Animator> ();
    }

    void Start ()
    {
        Destroy (gameObject, _animator.GetCurrentAnimatorStateInfo (0).length);
    }
}
