using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyFallenStar : YellowFallenStar
{
    [Header ("Yellow Fallen Star")]
    [SerializeField]
    public int starNumber;
    [SerializeField]
    YellowFallenStar _yellowStarPrefab;
    public float yellowHp;
    public float yellowSpeed;
    public int yellowDamage;
    public float yellowInfectedSpeed;

    Settings _settings;
    TheLight _theLight;

    public override void Awake ()
    {
        _settings = FindObjectOfType<Settings> ();
        _theLight = FindObjectOfType<TheLight> ();
        base.Awake ();
    }

    public override void OnDestroyed ()
    {
        if (infected)
        {
            _theHouse.OnHealed ();
        }
        else
        {
            _theHouse.OnConverted ();
            Instantiate<ParticleSystem> (_blowFx, transform.position, Quaternion.identity);
            SpawnYellowStar ();
        }
    }

    void SpawnYellowStar ()
    {
        while (starNumber-- > 0)
        {
            var specificHorizontalStep = _settings.GetSpecificHorizontalStep (_settings.specificHorizontalSteps);
            var spawnPointX = specificHorizontalStep;
            var spawnPoint = new Vector3 (spawnPointX, transform.position.y + Random.Range (0f, .5f), transform.position.z);
            var theStar = Instantiate<YellowFallenStar> (_yellowStarPrefab, spawnPoint, Quaternion.identity);
            theStar.hp = theStar.maxHp = yellowHp;
            theStar.speed = yellowSpeed;
            theStar.damage = yellowDamage;
            theStar.infectedSpeed = yellowInfectedSpeed;
            _theLight.AddStar (theStar);
        }
    }
}
