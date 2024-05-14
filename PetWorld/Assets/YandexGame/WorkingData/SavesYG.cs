
namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        public bool Sound = true;

        // Player

        public int Coins = 0;
        public int Energy = 10;
        public int Level = 1;
        public int Exp = 0;

        // Pets

        public int[] PetsCount = new int[50];
        public int[] PetsLevels = new int[50];
        public int Eat = 100;

        // Guns

        public bool[] WeaponPurchased = new bool[10];
        public int WeaponSelected = -1;
        public int WeaponDamage = 1;
        public int WeaponCrit = 1;
        public int ExtraShoots = 5;

        // Levels

        public int OpenIslandOnSampleScene = 0;
        public bool[] OpenLocations = new bool[5];
        public bool[,] CompletedLevels = new bool[5,10];
        public bool[,] OpenLevels = new bool[5, 10];

        // Time

        public string LastTimeLoad = "0";
        public float TimerEnergy = 0;
        public float TimerDayReward = 0;

        public float TimerForestCase = 0;
        public float TimerWaterCase = 0;
        public float TimerSnowCase = 0;
        public float TimerVolcanoCase = 0;
        public float TimerLightingCase = 0;

        // Other

        public int LastDayReward = -1;
        public bool DailyRewardIsReady = true;

        public bool ForestCaseReady = true;
        public bool WaterCaseReady = true;
        public bool SnowCaseReady = true;
        public bool VolcanoCaseReady = true;
        public bool LightingCaseReady = true;

        public bool ForestCaseWatchedAds = false;
        public bool WaterCaseWatchedAds = false;
        public bool SnowCaseWatchedAds = false;
        public bool VolcanoCaseWatchedAds = false;
        public bool LightingCaseWatchedAds = false;

        // Shop

        // Free rewards

        public int FreeRewardIndex = 0;
        public float TimerFreeReward = 0;
        public bool FreeRewardReady = true;

        public float TimerFreeCase = 0;
        public bool FreeCaseReady = true;

        // Super Offer

        public bool[] SuperOffersBought = new bool[5];

        // Learning

        public int CurrentLearningStep = 0;

        // Records

        public int Record = 0;

        // Вы можете выполнить какие то действия при загрузке сохранений
        public SavesYG()
        {
            OpenLocations[0] = true;
            OpenLevels[0, 0] = true;
            OpenLevels[1, 0] = true;
            OpenLevels[2, 0] = true;
            OpenLevels[3, 0] = true;
            OpenLevels[4, 0] = true;

            WeaponPurchased[0] = true;
        }
    }
}
