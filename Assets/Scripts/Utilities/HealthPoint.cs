using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPoint : MonoBehaviour
{
    [SerializeField]
    float _hp;
    [SerializeField]
    float _maxHp;
    public event System.Action OnDeath;

    public float hp
    {
        get
        {
            return _hp;
        }
    }

    public float maxHp
    {
        get
        {
            return _maxHp;
        }
    }

    public float normalize
    {
        get
        {
            return _hp / _maxHp;
        }
    }

    public void TakeDamage (float damage)
    {
        if (_hp < 0)
        {
            _hp = 0;
            return;
        }
        _hp -= damage;
        if (_hp > 0) return;
        if (OnDeath != null)
        {
            OnDeath ();
            return;
        }
    }

    public void Recovery (float amount)
    {
        _hp += amount;
        if (_hp < _maxHp) return;
        _hp = _maxHp;
    }

    public void SetMaxHp (float amount)
    {
        _maxHp = amount;
    }

    public void SetHp (float amount)
    {
        _hp = amount;
    }
}
