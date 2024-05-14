using System;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class DailyRewards : MonoBehaviour
{
    // UI

    [SerializeField] private Image[] rewardFields = new Image[7];

    [SerializeField] private GameObject buttonTakeReward;
    [SerializeField] private GameObject counterDays, textGetReward, counterOnSampleScene;
    [SerializeField] private GameObject redFlag;

    [SerializeField] private GetPetDailyReward getPetDailyReward;
    [SerializeField] private GameObject petRewardPanel;
    [SerializeField] private Image petImage;
    [SerializeField] private Image whiteArea;

    // Objects

    [SerializeField] private Sprite[] rareSprites = new Sprite[10];
    [SerializeField] private Sprite[] mythicSprites = new Sprite[5];

    [SerializeField] private PlayClip getReward, rarePet, mythicPet;

    private void Start()
    {
        Invoke("UpdateDailyPanel", 0.5f);
    }

    private void UpdateDailyPanel()
    {
        for (int i = 0; i < rewardFields.Length; i++)
        {
            if (YandexGame.savesData.LastDayReward >= i)
            {
                rewardFields[i].color = new Color32(0,255,0,150);
            }
            else if (YandexGame.savesData.LastDayReward + 1 == i && YandexGame.savesData.DailyRewardIsReady) 
            {
                rewardFields[i].color = new Color32(255, 255, 0, 150);
            }
            else
            {
                rewardFields[i].color = new Color32(255, 255, 255, 150);
            }
        }
    }

    private void Update()
    {
        if (YandexGame.savesData.DailyRewardIsReady)
        {
            if (counterDays.activeSelf) { UpdateDailyPanel(); }

            textGetReward.SetActive(true);
            counterDays.SetActive(false);
            counterOnSampleScene.SetActive(false);
            redFlag.SetActive(true);

            buttonTakeReward.GetComponent<Image>().color = Color.white;
            buttonTakeReward.GetComponent<Button>().enabled = true;
        }
        else
        {
            textGetReward.SetActive(false);
            counterDays.SetActive(true);
            counterOnSampleScene.SetActive(true);
            redFlag.SetActive(false);

            TimeSpan t = TimeSpan.FromSeconds(PlayerStats.playerStats.TimeLoadDailyRewards - YandexGame.savesData.TimerDayReward);

            counterDays.GetComponent<Text>().text = t.Hours + ":" + string.Format("{0:d2}", t.Minutes) + ":" + string.Format("{0:d2}", t.Seconds);
            counterOnSampleScene.GetComponent<Text>().text = t.Hours + ":" + string.Format("{0:d2}", t.Minutes) + ":" + string.Format("{0:d2}", t.Seconds);

            buttonTakeReward.GetComponent<Image>().color = new Color32(255,255,255,150);
            buttonTakeReward.GetComponent<Button>().enabled = false;
        }
    }

    public void GetReward()
    {
        YandexGame.savesData.DailyRewardIsReady = false;

        switch (YandexGame.savesData.LastDayReward)
        {
            case -1:
                YandexGame.savesData.Coins += 200;
                getReward.PlayOneShot();
                break;
            case 0:
                YandexGame.savesData.Eat += 100;
                getReward.PlayOneShot();
                break;
            case 1:
                YandexGame.savesData.ExtraShoots += 10;
                getReward.PlayOneShot();
                break;
            case 2:
                GetRandomPet();
                rarePet.PlayOneShot();
                break;
            case 3:
                YandexGame.savesData.Coins += 500;
                getReward.PlayOneShot();
                break;
            case 4:
                YandexGame.savesData.Eat += 250;
                getReward.PlayOneShot();
                break;
            case 5:
                GetRandomPet();
                mythicPet.PlayOneShot();
                break;
        }

        if (YandexGame.savesData.LastDayReward < 5) { YandexGame.savesData.LastDayReward++; }
        else { YandexGame.savesData.LastDayReward = -1; }
        PlayerStats.playerStats.UpdateStats();
        YandexGame.SaveProgress();

        UpdateDailyPanel();
    }

    public void GetRandomPet()
    {
        int lastLoc = 0;

        for(int i = 0; i < YandexGame.savesData.OpenLocations.Length; i++)
        {
            if (YandexGame.savesData.OpenLocations[i]) { lastLoc = i + 1; }
        }

        if (YandexGame.savesData.LastDayReward == 2)
        {
            int loc = UnityEngine.Random.Range(0, lastLoc);

            int pet = UnityEngine.Random.Range(0, 2);

            YandexGame.savesData.PetsCount[loc*10+pet+6]++;
            petImage.sprite = rareSprites[loc*2+pet];
            whiteArea.color = Color.green;
        }
        else if (YandexGame.savesData.LastDayReward == 5)
        {
            int loc = UnityEngine.Random.Range(0, lastLoc);

            YandexGame.savesData.PetsCount[loc * 10 + 8]++;
            petImage.sprite = mythicSprites[loc];
            whiteArea.color = new Color32(255, 0, 255, 255);
        }

        petRewardPanel.SetActive(true);
        getPetDailyReward.StartAnimation();
    }
}
