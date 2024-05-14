using System;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class CasePanelController : MonoBehaviour
{
    // UI Sample Scene View

    [SerializeField] private GameObject redFlag, sampleTimer, roulettePanel;

    // UI Main View

    [SerializeField] private GameObject casePanel;
    [SerializeField] private GameObject[] cases = new GameObject[5];
    [SerializeField] private Text[] timerTexts = new Text[5];
    [SerializeField] private GameObject[] readyTexts = new GameObject[5];
    [SerializeField] private GameObject[] lockedTexts = new GameObject[5];
    [SerializeField] private GameObject[] timeTexts = new GameObject[5];

    // UI Personal Panel

    [SerializeField] private GameObject eggPanel;
    [SerializeField] private Image eggImage;
    [SerializeField] private GameObject buttonOpen;
    [SerializeField] private GameObject textOpen, textTime;
    [SerializeField] private GameObject buttonShowAds;

    // Objects

    [SerializeField] private Animator rouletteAnim;
    private int openCaseIndex = 0;
    [SerializeField] private Sprite[] eggSprites = new Sprite[5];

    [SerializeField] private Sprite[] petsSprites = new Sprite[50];

    [SerializeField] private RouletteSound[] rouletteSounds;
    [SerializeField] private Image[] petsDropLine = new Image[38];
    [SerializeField] private Image[] rareDropLine = new Image[38]; // Index = 32 (33 pet) is drops
    [SerializeField] private Image petDrop;
    [SerializeField] private Image raysDrop;

    [SerializeField] private PlayClip getCommonPet, getRarePet, getMythicPet, getLegendaryPet;

    private void Update()
    {
        if (!roulettePanel.activeSelf)
        {
            if (GetMinTimer() == 0) 
            {
                redFlag.SetActive(true);
                sampleTimer.SetActive(false);
            }
            else
            {
                redFlag.SetActive(false);
                sampleTimer.SetActive(true);

                TimeSpan t = TimeSpan.FromSeconds(GetMinTimer());
                sampleTimer.GetComponent<Text>().text = t.Minutes + ":" + string.Format("{0:d2}", t.Seconds);
            }
        }


        if (casePanel.activeSelf)
        {
            UpdateCasePanelTimer(YandexGame.savesData.ForestCaseReady, PlayerStats.playerStats.TimeLoadCase - YandexGame.savesData.TimerForestCase, 0);
            UpdateCasePanelTimer(YandexGame.savesData.WaterCaseReady, PlayerStats.playerStats.TimeLoadCase - YandexGame.savesData.TimerWaterCase, 1);
            UpdateCasePanelTimer(YandexGame.savesData.SnowCaseReady, PlayerStats.playerStats.TimeLoadCase - YandexGame.savesData.TimerSnowCase, 2);
            UpdateCasePanelTimer(YandexGame.savesData.VolcanoCaseReady, PlayerStats.playerStats.TimeLoadCase - YandexGame.savesData.TimerVolcanoCase, 3);
            UpdateCasePanelTimer(YandexGame.savesData.LightingCaseReady, PlayerStats.playerStats.TimeLoadCase - YandexGame.savesData.TimerLightingCase, 4);
        }
        else
        {
            switch (openCaseIndex)
            {
                case 0:
                    UpdatePersonalCaseTimer(YandexGame.savesData.ForestCaseReady, PlayerStats.playerStats.TimeLoadCase - YandexGame.savesData.TimerForestCase, 0, 
                        YandexGame.savesData.ForestCaseWatchedAds);
                    break;
                case 1:
                    UpdatePersonalCaseTimer(YandexGame.savesData.WaterCaseReady, PlayerStats.playerStats.TimeLoadCase - YandexGame.savesData.TimerWaterCase, 1,
                        YandexGame.savesData.WaterCaseWatchedAds);
                    break;
                case 2:
                    UpdatePersonalCaseTimer(YandexGame.savesData.SnowCaseReady, PlayerStats.playerStats.TimeLoadCase - YandexGame.savesData.TimerSnowCase, 2,
                        YandexGame.savesData.SnowCaseWatchedAds);
                    break;
                case 3:
                    UpdatePersonalCaseTimer(YandexGame.savesData.VolcanoCaseReady, PlayerStats.playerStats.TimeLoadCase - YandexGame.savesData.TimerVolcanoCase, 3,
                        YandexGame.savesData.VolcanoCaseWatchedAds);
                    break;
                case 4:
                    UpdatePersonalCaseTimer(YandexGame.savesData.LightingCaseReady, PlayerStats.playerStats.TimeLoadCase - YandexGame.savesData.TimerLightingCase, 4,
                        YandexGame.savesData.LightingCaseWatchedAds);
                    break;
            }
        }
    }

    private void UpdateCasePanelTimer(bool isReady, float timeBefore, int index)
    {
        if (YandexGame.savesData.OpenLocations[index])
        {
            cases[index].GetComponent<Image>().color = Color.white;
            cases[index].GetComponent<Button>().enabled = true;
            lockedTexts[index].SetActive(false);

            if (isReady)
            {
                readyTexts[index].SetActive(true);
                timeTexts[index].SetActive(false);
            }
            else
            {
                readyTexts[index].SetActive(false);
                timeTexts[index].SetActive(true);

                TimeSpan t = TimeSpan.FromSeconds(timeBefore);
                timerTexts[index].text = t.Minutes + ":" + string.Format("{0:d2}", t.Seconds);
            }
        }
        else
        {
            lockedTexts[index].SetActive(true);
            readyTexts[index].SetActive(false);
            timeTexts[index].SetActive(false);
            cases[index].GetComponent<Image>().color = Color.black;
            cases[index].GetComponent<Button>().enabled = false;
        }
    }

    private void UpdatePersonalCaseTimer(bool isReady, float timeBefore, int index, bool watchedAds)
    {

        if (isReady)
        {
            buttonOpen.GetComponent<Button>().enabled = true;
            buttonOpen.GetComponent<Image>().color = new Color32(255,255,255,255);
            textOpen.SetActive(true);
            textTime.SetActive(false);

            buttonShowAds.SetActive(false);
        }
        else
        {
            buttonOpen.GetComponent<Button>().enabled = false;
            buttonOpen.GetComponent<Image>().color = new Color32(255, 255, 255, 150);
            textOpen.SetActive(false);
            textTime.SetActive(true);

            if (!watchedAds) { buttonShowAds.SetActive(true); }

            TimeSpan t = TimeSpan.FromSeconds(timeBefore);
            textTime.GetComponent<Text>().text = t.Minutes + ":" + string.Format("{0:d2}", t.Seconds);
        }
    }

    public void ChooseContainer(int index)
    {
        openCaseIndex = index;

        eggImage.sprite = eggSprites[openCaseIndex];

        eggPanel.SetActive(true);
        casePanel.SetActive(false);
    }

    public void SkipTimeForAds()
    {
        RewardedAds.instance.SkipCaseTime(openCaseIndex);
    }

    public void OpenCase()
    {
        foreach (RouletteSound rS in rouletteSounds)
        {
            rS.alreadyPlayed = false;
        }
        SetAllDrop();
        rouletteAnim.SetBool("Start", true);
    }

    public void StopAnim()
    {
        switch (openCaseIndex)
        {
            case 0:
                YandexGame.savesData.TimerForestCase = 0;
                YandexGame.savesData.ForestCaseReady = false;
                break;
            case 1:
                YandexGame.savesData.TimerWaterCase = 0;
                YandexGame.savesData.WaterCaseReady = false;
                break;
            case 2:
                YandexGame.savesData.TimerSnowCase = 0;
                YandexGame.savesData.SnowCaseReady = false;
                break;
            case 3:
                YandexGame.savesData.TimerVolcanoCase = 0;
                YandexGame.savesData.VolcanoCaseReady = false;
                break;
            case 4:
                YandexGame.savesData.TimerLightingCase = 0;
                YandexGame.savesData.LightingCaseReady = false;
                break;
        }

        YandexGame.SaveProgress();
        rouletteAnim.SetBool("Start", false);
        PlayerStats.playerStats.UpdateStats();
    }

    private void SetAllDrop()
    {
        for (int i = 0; i < petsDropLine.Length; i++)
        {

            int rand = UnityEngine.Random.Range(0, 100);

            switch (rand)
            {
                case < 75:
                    int common = UnityEngine.Random.Range(0, 6);
                    petsDropLine[i].sprite = petsSprites[openCaseIndex * 10 + common];
                    rareDropLine[i].color = Color.white;

                    if (i == 32)
                    {
                        raysDrop.color = Color.white;
                        petDrop.sprite = petsSprites[openCaseIndex * 10 + common];
                        YandexGame.savesData.PetsCount[openCaseIndex * 10 + common]++;
                        YandexGame.SaveProgress();

                        Invoke("PlayCommonPet", 11);
                    }
                    break;

                case < 94:
                    int rare = UnityEngine.Random.Range(6, 8);
                    petsDropLine[i].sprite = petsSprites[openCaseIndex * 10 + rare];
                    rareDropLine[i].color = Color.green;

                    if (i == 32)
                    {
                        raysDrop.color = Color.green;
                        petDrop.sprite = petsSprites[openCaseIndex * 10 + rare];
                        YandexGame.savesData.PetsCount[openCaseIndex * 10 + rare]++;
                        YandexGame.SaveProgress();

                        Invoke("PlayRarePet", 11);
                    }
                    break;

                case < 99:
                    petsDropLine[i].sprite = petsSprites[openCaseIndex * 10 + 8];
                    rareDropLine[i].color = new Color32(255, 0, 255, 255);

                    if (i == 32)
                    {
                        raysDrop.color = new Color32(255, 0, 255, 255);
                        petDrop.sprite = petsSprites[openCaseIndex * 10 + 8];
                        YandexGame.savesData.PetsCount[openCaseIndex * 10 + 8]++;
                        YandexGame.SaveProgress();

                        Invoke("PlayMythicPet", 11);
                    }
                    break;

                case < 100:
                    petsDropLine[i].sprite = petsSprites[openCaseIndex * 10 + 9];
                    rareDropLine[i].color = Color.yellow;

                    if (i == 32)
                    {
                        raysDrop.color = Color.yellow;
                        petDrop.sprite = petsSprites[openCaseIndex * 10 + 9];
                        YandexGame.savesData.PetsCount[openCaseIndex * 10 + 9]++;
                        YandexGame.SaveProgress();

                        Invoke("PlayLegendaryPet", 11);
                    }
                    break;
            }
        }
    }

    private float GetMinTimer()
    {
        if (YandexGame.savesData.ForestCaseReady && YandexGame.savesData.OpenLocations[0] ||
            YandexGame.savesData.WaterCaseReady && YandexGame.savesData.OpenLocations[1] ||
            YandexGame.savesData.SnowCaseReady && YandexGame.savesData.OpenLocations[2] ||
            YandexGame.savesData.VolcanoCaseReady && YandexGame.savesData.OpenLocations[3] ||
            YandexGame.savesData.LightingCaseReady && YandexGame.savesData.OpenLocations[4])
        {
            return 0;
        }

        float minTime = PlayerStats.playerStats.TimeLoadCase - YandexGame.savesData.TimerForestCase;
        if (minTime <= 0) { minTime = 0; }

        if (PlayerStats.playerStats.TimeLoadCase - YandexGame.savesData.TimerWaterCase < minTime &&
            PlayerStats.playerStats.TimeLoadCase - YandexGame.savesData.TimerWaterCase >= 0) { minTime = PlayerStats.playerStats.TimeLoadCase - YandexGame.savesData.TimerWaterCase; }
        if (PlayerStats.playerStats.TimeLoadCase - YandexGame.savesData.TimerSnowCase < minTime &&
            PlayerStats.playerStats.TimeLoadCase - YandexGame.savesData.TimerSnowCase >= 0) { minTime = PlayerStats.playerStats.TimeLoadCase - YandexGame.savesData.TimerSnowCase; }
        if (PlayerStats.playerStats.TimeLoadCase - YandexGame.savesData.TimerVolcanoCase < minTime &&
            PlayerStats.playerStats.TimeLoadCase - YandexGame.savesData.TimerVolcanoCase >= 0) { minTime = PlayerStats.playerStats.TimeLoadCase - YandexGame.savesData.TimerVolcanoCase; }
        if (PlayerStats.playerStats.TimeLoadCase - YandexGame.savesData.TimerLightingCase < minTime &&
            PlayerStats.playerStats.TimeLoadCase - YandexGame.savesData.TimerLightingCase >= 0) { minTime = PlayerStats.playerStats.TimeLoadCase - YandexGame.savesData.TimerLightingCase; }
        return minTime;
    }

    private void PlayCommonPet()
    {

        getCommonPet.PlayOneShot();
    }

    private void PlayRarePet()
    {

        getRarePet.PlayOneShot();
    }

    private void PlayMythicPet()
    {

        getMythicPet.PlayOneShot();
    }

    private void PlayLegendaryPet()
    {

        getLegendaryPet.PlayOneShot();
    }
}
