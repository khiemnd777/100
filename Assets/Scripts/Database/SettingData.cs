using UnityEngine;

[CreateAssetMenu (fileName = "SettingData", menuName = "Database/Setting")]
public class SettingData : ScriptableObject
{
    public int startCount;
    public int killedCount;
    public int finishGameCount;
    public bool unlockTheDaystar;
    public bool alreadySwipeTutorial;
    public bool alreadySpecialSkillTutorial;
    public bool vibrated;
    public bool slowmotion;
    public int videoAdsCooldown;
    public int videoAdsCount;

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

    public bool CanShowVideoAds ()
    {
        if (videoAdsCount == videoAdsCooldown - 1)
        {
            videoAdsCount = 0;
            return true;
        }
        ++videoAdsCount;
        return false;
    }
}
