using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class TheLightSpawner : MonoBehaviour
{
    List<TheStar> _theStars = new List<TheStar> ();
    public List<TheStar> theStars
    {
        get
        {
            return _theStars.Where (s => s).ToList ();
        }
    }

    public abstract IEnumerator Spawn ();

    public void AddStar (TheStar theStar)
    {
        _theStars.Add (theStar);
    }

    public virtual void SelfDestructTheStars ()
    {
        var stars = theStars;
        stars.ForEach (s => s.SelfDestruct ());
    }
}
