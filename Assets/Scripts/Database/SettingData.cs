using UnityEngine;

[CreateAssetMenu (fileName = "SettingData", menuName = "Database/Setting")]
public class SettingData : ScriptableObject
{
    public int startCount;
    public int killedCount;
    public int finishGameCount;
    public bool alreadySwipeTutorial;
    public bool alreadySpecialSkillTutorial;

    public void IncreaseKilledCount ()
    {
        ++killedCount;
    }

    public void IncreaseKilledCount (int amount)
    {
        killedCount += amount;
    }

    public void IncreaseFinishGameCount ()
    {
        ++finishGameCount;
    }

    public void ResetKilledCount ()
    {
        killedCount = 0;
    }
}
