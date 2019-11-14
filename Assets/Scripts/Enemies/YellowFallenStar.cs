using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowFallenStar : TheStar
{
    [SerializeField]
    float _unfreezeDelay;
    [Header ("The Infected")]
    public bool infected;
    public float infectedSpeed;
    public Sprite infectedSprite;
    public Material infectedParticleMaterial;
    [Space]
    public ParticleSystemRenderer particleRenderer;
    public ParticleSystem particle;
    ObjectShake _objectShake;
    protected TheHouse _theHouse;
    protected TheHellFire _theHellFire;
    [SerializeField]
    protected ParticleSystem _blowFx;
    [SerializeField]
    AudioSource _deathSoundFxAtCollision;
    [SerializeField]
    AudioSource _playerSoundFxAtCollision;
    CameraShake _shakeCamera;
    bool _freeze;

    public override void Awake ()
    {
        base.Awake ();
        _shakeCamera = FindObjectOfType<CameraShake> ();
        _objectShake = GetComponent<ObjectShake> ();
        _theHouse = FindObjectOfType<TheHouse> ();
        _theHellFire = FindObjectOfType<TheHellFire> ();
    }

    public override void Update ()
    {
        base.Update ();
        FallDown ();
    }

    void FallDown ()
    {
        if (_freeze) return;
        transform.Translate (Vector3.down * Time.deltaTime * (speed / 10f));
    }

    protected void InstantiateDeathSoundEffectAtCollision ()
    {
        if (!_deathSoundFxAtCollision) return;
        Instantiate (_deathSoundFxAtCollision, transform.position, Quaternion.identity);
    }

    void InstantiatePlayerSoundEffectAtCollision ()
    {
        if (!_playerSoundFxAtCollision) return;
        Instantiate (_playerSoundFxAtCollision, transform.position, Quaternion.identity);
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.tag == "The House")
        {
            if (!infected)
            {
                if (_theHouse.sheep <= 0)
                {
                    Destroy (gameObject);
                }
                var goOutPoint = _theHouse.goOutPoint;
                infected = true;
                display.sprite = infectedSprite;
                particleRenderer.material = infectedParticleMaterial;
                transform.localScale = Vector3.zero;
                // speed = 0f;
                var particleMain = particle.main;
                particleMain.loop = false;
                // transform.position = new Vector3 (transform.position.x, goOutPoint.position.y, transform.position.z);
                StartCoroutine (GoingOut (goOutPoint));
            }
        }
        else if (other.tag == "Player")
        {
            other.GetComponent<TheShepherd> ().Hit (damage);
            _shakeCamera.Shake (.175f, .065f, true);
            Instantiate<ParticleSystem> (_blowFx, transform.position, Quaternion.identity);
            InstantiatePlayerSoundEffectAtCollision ();
            Destroy (gameObject);
        }
        else if (other.tag == "The Hell Fire")
        {
            if (infected)
            {
                _theHellFire.OnEntered ();
                Destroy (gameObject);
            }
        }
    }

    IEnumerator GoingOut (Transform goOutPoint)
    {
        yield return new WaitForSeconds (1f);
        var particleMain = particle.main;
        particleMain.loop = true;
        particle.Play ();
        speed = infectedSpeed;
        transform.localScale = Vector3.one;
        gameObject.SetActive (true);
        transform.position = new Vector3 (transform.position.x, goOutPoint.position.y, transform.position.z);
        _theHouse.OnInfected ();
    }

    public override void OnHit (float damage, Transform damagedBy)
    {
        _freeze = true;
        _objectShake.Shake ();
        base.OnHit (damage, damagedBy);
        StartCoroutine (Unfreeze ());
    }

    IEnumerator Unfreeze ()
    {
        yield return new WaitForSeconds (_unfreezeDelay);
        _freeze = false;
    }

    public override void OnDestroyed (float damage, Transform damagedBy)
    {
        if (infected)
        {
            _settingData.IncreaseKilledCount (5);
            if (!"Radioactive".Equals (damagedBy.tag))
            {
                _theHouse.OnHealed ();
            }
            Instantiate<ParticleSystem> (_blowFx, transform.position, Quaternion.identity);
            InstantiateDeathSoundEffectAtCollision ();
        }
        else
        {
            _settingData.IncreaseKilledCount ();
            if (!"Radioactive".Equals (damagedBy.tag))
            {
                _theHouse.OnConverted ();
            }
            Instantiate<ParticleSystem> (_blowFx, transform.position, Quaternion.identity);
            InstantiateDeathSoundEffectAtCollision ();
        }
    }

    public override void SelfDestruct ()
    {
        Instantiate<ParticleSystem> (_blowFx, transform.position, Quaternion.identity);
        Destroy (gameObject);
    }
}
