using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public float hp;
    public float maxHp;

    public virtual void Awake ()
    {

    }

    public virtual void Start ()
    {

    }

    public virtual void Update ()
    {

    }

    public virtual void FixedUpdate ()
    {

    }

    public virtual void OnHit (float damage, Transform damagedBy)
    {
        hp -= damage;
        if (hp <= 0f)
        {
            OnDestroyed ();
            Destroy (gameObject);
        }
    }

    public virtual void OnDestroyed ()
    {

    }
}
