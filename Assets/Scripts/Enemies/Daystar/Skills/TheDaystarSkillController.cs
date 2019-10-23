using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheDaystarSkillController : MonoBehaviour
{
    [SerializeField]
    List<DaystarSkill> _skills;

    void Awake ()
    {
        _skills.ForEach (skill => skill.onEnd += OnSkillEnd);
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
        yield return new WaitForSeconds (1.15f);
        var nextSkill = GetNextSkill ();
        nextSkill.Execute ();
    }
}
