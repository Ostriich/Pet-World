using UnityEngine;
using YG;

public class PlayerStats : MonoBehaviour
{
    // Singleton set

    public static PlayerStats playerStats;

    private void Awake()
    {
        if (!playerStats)
        {
            playerStats = this;
            DontDestroyOnLoad(this);
        }
        else 
        {
            Destroy(gameObject);
        }
    }

    // Stats

    public int HealthPlayer;
    public int CurrentHealthPlayer;
    public int AttackPlayer;
    public float TimeLoadEnergy = 60;
    public float TimeLoadDailyRewards = 86400;
    public float TimeLoadCase = 900;
    public float TimeLoadFreeReward = 14400;
    public float TimeLoadFreeCase = 43200;

    // Level

    public int LevelIndex = 0;

    private void Start()
    {
        //YandexGame.ResetSaveProgress();

        UpdateStats();

        YandexGame.savesData.TimerEnergy += TimeMasterNRG.instance.CheckDate();
        if (!YandexGame.savesData.DailyRewardIsReady) { YandexGame.savesData.TimerDayReward += TimeMasterNRG.instance.CheckDate(); }
        YandexGame.savesData.TimerForestCase += TimeMasterNRG.instance.CheckDate();
        YandexGame.savesData.TimerWaterCase += TimeMasterNRG.instance.CheckDate();
        YandexGame.savesData.TimerSnowCase += TimeMasterNRG.instance.CheckDate();
        YandexGame.savesData.TimerVolcanoCase += TimeMasterNRG.instance.CheckDate();
        YandexGame.savesData.TimerLightingCase += TimeMasterNRG.instance.CheckDate();
        if (!YandexGame.savesData.FreeRewardReady) YandexGame.savesData.TimerFreeReward += TimeMasterNRG.instance.CheckDate();
        YandexGame.savesData.TimerFreeCase += TimeMasterNRG.instance.CheckDate();

        TimeMasterNRG.instance.SaveDate();
    }

    public void UpdateStats()
    {
        HealthPlayer = 0;
        AttackPlayer = 0;
        for (int i = 0; i < YandexGame.savesData.PetsCount.Length; i++)
        {
            if (YandexGame.savesData.PetsCount[i] != 0)
            {
                switch (i % 10)
                {
                    case < 6:
                        AttackPlayer += (75 + YandexGame.savesData.PetsLevels[i] * 5);
                        HealthPlayer += (50 + YandexGame.savesData.PetsLevels[i] * 10);
                        break;
                    case < 8:
                        AttackPlayer += (100 + YandexGame.savesData.PetsLevels[i] * 7);
                        HealthPlayer += (100 + YandexGame.savesData.PetsLevels[i] * 5);
                        break;
                    case 8:
                        AttackPlayer += (150 + YandexGame.savesData.PetsLevels[i] * 10);
                        HealthPlayer += (200 + YandexGame.savesData.PetsLevels[i] * 12);
                        break;
                    case 9:
                        AttackPlayer += (200 + YandexGame.savesData.PetsLevels[i] * 15);
                        HealthPlayer += (300 + YandexGame.savesData.PetsLevels[i] * 15);
                        break;
                }
            }
        }

        if (YandexGame.savesData.Record < HealthPlayer + AttackPlayer) 
        { 
            YandexGame.NewLeaderboardScores("LB", HealthPlayer + AttackPlayer);
            YandexGame.savesData.Record = HealthPlayer + AttackPlayer;
            YandexGame.SaveProgress();
        }

        CurrentHealthPlayer = HealthPlayer;
    }

    private void FixedUpdate()
    {
        // Energy

        if (YandexGame.savesData.Energy < (10 + YandexGame.savesData.Level / 5 * 5))
        {
            YandexGame.savesData.TimerEnergy += Time.deltaTime;

            if (YandexGame.savesData.TimerEnergy >= TimeLoadEnergy)
            {
                YandexGame.savesData.Energy++;
                YandexGame.savesData.TimerEnergy -= TimeLoadEnergy;
                
                if (YandexGame.savesData.TimerEnergy < TimeLoadEnergy) { TimeMasterNRG.instance.SaveDate(); }
            }
        }
        else
        {
            YandexGame.savesData.TimerEnergy = 0;
        }

        // Daily reward

        if (!YandexGame.savesData.DailyRewardIsReady)
        {
            YandexGame.savesData.TimerDayReward += Time.deltaTime;

            if (YandexGame.savesData.TimerDayReward >= TimeLoadDailyRewards)
            {
                YandexGame.savesData.DailyRewardIsReady = true;
                YandexGame.savesData.TimerDayReward = 0;
                TimeMasterNRG.instance.SaveDate();
            }
        }

        // Egg cases

        // Forest

        if (!YandexGame.savesData.ForestCaseReady)
        {
            YandexGame.savesData.TimerForestCase += Time.deltaTime;

            if (YandexGame.savesData.TimerForestCase >= TimeLoadCase)
            {
                YandexGame.savesData.ForestCaseWatchedAds = false;
                YandexGame.savesData.ForestCaseReady = true;
                YandexGame.savesData.TimerForestCase = 0;
                TimeMasterNRG.instance.SaveDate();
            }
        }

        // Water

        if (!YandexGame.savesData.WaterCaseReady)
        {
            YandexGame.savesData.TimerWaterCase += Time.deltaTime;

            if (YandexGame.savesData.TimerWaterCase >= TimeLoadCase)
            {
                YandexGame.savesData.WaterCaseWatchedAds = false;
                YandexGame.savesData.WaterCaseReady = true;
                YandexGame.savesData.TimerWaterCase = 0;
                TimeMasterNRG.instance.SaveDate();
            }
        }

        // Snow

        if (!YandexGame.savesData.SnowCaseReady)
        {
            YandexGame.savesData.TimerSnowCase += Time.deltaTime;

            if (YandexGame.savesData.TimerSnowCase >= TimeLoadCase)
            {
                YandexGame.savesData.SnowCaseWatchedAds = false;
                YandexGame.savesData.SnowCaseReady = true;
                YandexGame.savesData.TimerSnowCase = 0;
                TimeMasterNRG.instance.SaveDate();
            }
        }

        // Volcano

        if (!YandexGame.savesData.VolcanoCaseReady)
        {
            YandexGame.savesData.TimerVolcanoCase += Time.deltaTime;

            if (YandexGame.savesData.TimerVolcanoCase >= TimeLoadCase)
            {
                YandexGame.savesData.VolcanoCaseWatchedAds = false;
                YandexGame.savesData.VolcanoCaseReady = true;
                YandexGame.savesData.TimerVolcanoCase = 0;
                TimeMasterNRG.instance.SaveDate();
            }
        }

        // Lighting

        if (!YandexGame.savesData.LightingCaseReady)
        {
            YandexGame.savesData.TimerLightingCase += Time.deltaTime;

            if (YandexGame.savesData.TimerLightingCase >= TimeLoadCase)
            {
                YandexGame.savesData.LightingCaseWatchedAds = false;
                YandexGame.savesData.LightingCaseReady = true;
                YandexGame.savesData.TimerLightingCase = 0;
                TimeMasterNRG.instance.SaveDate();
            }
        }

        // Shop

        // Free Reward

        if (!YandexGame.savesData.FreeRewardReady)
        {
            YandexGame.savesData.TimerFreeReward += Time.deltaTime;

            if (YandexGame.savesData.TimerFreeReward >= TimeLoadFreeReward)
            {
                YandexGame.savesData.FreeRewardReady = true;
                YandexGame.savesData.TimerFreeReward = 0;
                TimeMasterNRG.instance.SaveDate();
            }
        }

        // Free Case

        if (!YandexGame.savesData.FreeCaseReady)
        {
            YandexGame.savesData.TimerFreeCase += Time.deltaTime;

            if (YandexGame.savesData.TimerFreeCase >= TimeLoadFreeCase)
            {
                YandexGame.savesData.FreeCaseReady = true;
                YandexGame.savesData.TimerFreeCase = 0;
                TimeMasterNRG.instance.SaveDate();
            }
        }
    }
}
