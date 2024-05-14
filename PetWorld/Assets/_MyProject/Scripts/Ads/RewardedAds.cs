using UnityEngine;
using YG;

public class RewardedAds : MonoBehaviour
{
    // Singleton set

    public static RewardedAds instance;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private int rewardedId;

    public void SkipCaseTime(int i)
    {
        rewardedId = i;
        ShowRewarded(1);
    }

    public void GetReward()
    {
        switch (rewardedId)
        {
            case 0:
                YandexGame.savesData.ForestCaseWatchedAds = true;
                YandexGame.savesData.ForestCaseReady = true;
                YandexGame.savesData.TimerForestCase = 0;
                break;
            case 1:
                YandexGame.savesData.WaterCaseWatchedAds = true;
                YandexGame.savesData.WaterCaseReady = true;
                YandexGame.savesData.TimerWaterCase = 0;
                break;
            case 2:
                YandexGame.savesData.SnowCaseWatchedAds = true;
                YandexGame.savesData.SnowCaseReady = true;
                YandexGame.savesData.TimerSnowCase = 0;
                break;
            case 3:
                YandexGame.savesData.VolcanoCaseWatchedAds = true;
                YandexGame.savesData.VolcanoCaseReady = true;
                YandexGame.savesData.TimerVolcanoCase = 0;
                break;
            case 4:
                YandexGame.savesData.LightingCaseWatchedAds = true;
                YandexGame.savesData.LightingCaseReady = true;
                YandexGame.savesData.TimerLightingCase = 0;
                break;
            case 5:
                YandexGame.savesData.Energy = 10 + YandexGame.savesData.Level / 5 * 5;
                YandexGame.SaveProgress();
                PlayerStats.playerStats.UpdateStats();
                break;
            case 6:
                YandexGame.savesData.ExtraShoots +=3;
                YandexGame.SaveProgress();
                break;
        }

        TimeMasterNRG.instance.SaveDate();
    }

    private void ShowRewarded(int id)
    {
        YandexGame.RewVideoShow(id);
    }
}
