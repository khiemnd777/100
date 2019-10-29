using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheDaystarSkillController : MonoBehaviour
{
    [SerializeField]
    List<DaystarSkill> _skills;
    TheDaystar _theDaystar;

    void Awake ()
    {
        _skills.ForEach (skill => skill.onEnd += OnSkillEnd);
        _theDaystar = GetComponent<TheDaystar> ();
    }

    public void Play ()
    {
        OnSkillEnd ();
    }

    void OnSkillEnd ()
    {
        StartCoroutine (Next ());
    }

    DaystarSkill GetNextSkill ()
    {
        var skill = _skills[Random.Range (0, _skills.Count)];
        return skill;
    }

    IEnumerator Next ()
    {
        var timeToNext = _theDaystar.GetNormalizeHp () <= (1f / 7f)?.25f : 1.15f;
        yield return new WaitForSeconds (timeToNext);
        var nextSkill = GetNextSkill ();
        nextSkill.Execute ();
    }
}
