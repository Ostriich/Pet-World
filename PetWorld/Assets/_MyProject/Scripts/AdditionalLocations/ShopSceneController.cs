using System;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class ShopSceneController : MonoBehaviour
{
    [Header ("____________________ Free reward ____________________")]
    [Space (20)]

    [SerializeField] private Image reward;
    [SerializeField] private Text countOfReward;
    [SerializeField] private Sprite[] rewardSprites = new Sprite[3];
    [SerializeField] private GameObject buttonGetReward;
    [SerializeField] private GameObject textTimerFreeReward, textGetFreeReward;

    [SerializeField] private int[] startValueReward = new int[3];
    [SerializeField] private int[] deltaValueReward = new int[3];
    private int freeRewardValue;
    [SerializeField] private GameObject contentPos;

    [Header("____________________ Super Offer ____________________")]
    [Space(20)]

    [SerializeField] private Image petSuperOffer;
    [SerializeField] private Sprite[] spritesLegendaryPets = new Sprite[5];
    [SerializeField] private Text coinsOfferText, foodOfferText;
    [SerializeField] private GameObject hideOffer, showOffer;
    [SerializeField] private GameObject getPet;
    [SerializeField] private Image offerPetImage;

    private int lastOpenLocationIndex;

    [Header("____________________ Cases ____________________")]
    [Space(20)]

    [SerializeField] private int[] costCase = new int[4];
    [SerializeField] private GameObject[] buttonsGetCase = new GameObject[3];
    [SerializeField] private GameObject textTimerFreeCase, textGetFreeCase;

    [SerializeField] private GameObject infoCasePanel;
    [SerializeField] private Text[] rewards = new Text[6];
    [SerializeField] private GameObject[] nameCases = new GameObject[4];
    [SerializeField] private Image eggImage, shadowImage;

    private string[,] rewardStrings = new string[4, 6] 
    {   { "25 - 50", "20 - 30", "x 2", "x 0", "x 0", "x 0" },
        {  "50 - 100", "40 - 50", "x 2", "x 1", "x 0", "x 0" },
        {  "100 - 300", "70 - 90", "x 1", "x 2", "x 1", "x 0" },
        {  "300 - 500", "125 - 150", "x 0", "x 1", "x 2", "x 1" } };


    [SerializeField] private Sprite[] eggSprites = new Sprite[4];

    [Header("____________________ OpenCasePanel ____________________")]
    [Space(20)]

    [SerializeField] private GameObject casePanel;
    [SerializeField] private Image eggCase, areaCase;
    [SerializeField] private OpenCasePanel openCasePanel;
    [SerializeField] private Text coinsDropValue;
    [SerializeField] private Text foodDropValue;
    [SerializeField] private Image[] areasOfDropPets = new Image[4];
    [SerializeField] private Image[] imagesDropPets = new Image[4];
    [SerializeField] private Sprite[] petSprites = new Sprite[50];

    [Header("____________________ Other ____________________")]
    [Space(20)]

    [SerializeField] private PlayClip legendaryPetSound;

    private void Start()
    {
        contentPos.transform.localPosition = new Vector3(0, -1200, 0);
    }

    private void Update()
    {
        // Free reward

        reward.sprite = rewardSprites[YandexGame.savesData.FreeRewardIndex];

        freeRewardValue = startValueReward[YandexGame.savesData.FreeRewardIndex];
        for (int i = 0; i < YandexGame.savesData.OpenLocations.Length; i++)
        {
            if (YandexGame.savesData.OpenLocations[i]) 
            { 
                freeRewardValue += deltaValueReward[YandexGame.savesData.FreeRewardIndex];

                // part of super offeer
                lastOpenLocationIndex = i;
            }
        }

        countOfReward.text = ShortCutValue.shortCutValue.CutIntValue(freeRewardValue, 1);

        if (!YandexGame.savesData.FreeRewardReady)
        {
            buttonGetReward.GetComponent<Button>().enabled = false;
            buttonGetReward.GetComponent<Image>().color = new Color32(255, 255, 255, 150);

            textGetFreeReward.SetActive(false);
            textTimerFreeReward.SetActive(true);

            TimeSpan t = TimeSpan.FromSeconds(PlayerStats.playerStats.TimeLoadFreeReward - YandexGame.savesData.TimerFreeReward);
            textTimerFreeReward.GetComponent<Text>().text = t.Hours + ":" + string.Format("{0:d2}", t.Minutes) + ":" + string.Format("{0:d2}", t.Seconds);
        }
        else
        {
            buttonGetReward.GetComponent<Button>().enabled = true;
            buttonGetReward.GetComponent<Image>().color = new Color32(255, 255, 255, 255);

            textGetFreeReward.SetActive(true);
            textTimerFreeReward.SetActive(false);
        }

        // Super Offer

        if ( !YandexGame.savesData.SuperOffersBought[lastOpenLocationIndex] )
        {
            petSuperOffer.sprite = spritesLegendaryPets[lastOpenLocationIndex];
            coinsOfferText.text = ShortCutValue.shortCutValue.CutIntValue(1000 + lastOpenLocationIndex * 1000, 0);
            foodOfferText.text = ShortCutValue.shortCutValue.CutIntValue(200 + lastOpenLocationIndex * 200, 1);

            hideOffer.SetActive(false);
            showOffer.SetActive(true);
        }
        else 
        {
            hideOffer.SetActive(true);
            showOffer.SetActive(false);
        }

        // Cases

        // Free Case

        if (!YandexGame.savesData.FreeCaseReady)
        {
            buttonsGetCase[0].GetComponent<Button>().enabled = false;
            buttonsGetCase[0].GetComponent<Image>().color = new Color32(255, 255, 255, 150);

            textGetFreeCase.SetActive(false);
            textTimerFreeCase.SetActive(true);

            TimeSpan t = TimeSpan.FromSeconds(PlayerStats.playerStats.TimeLoadFreeCase - YandexGame.savesData.TimerFreeCase);
            textTimerFreeCase.GetComponent<Text>().text = t.Hours + ":" + string.Format("{0:d2}", t.Minutes) + ":" + string.Format("{0:d2}", t.Seconds);
        }
        else
        {
            buttonsGetCase[0].GetComponent<Button>().enabled = true;
            buttonsGetCase[0].GetComponent<Image>().color = new Color32(255, 255, 255, 255);

            textGetFreeCase.SetActive(true);
            textTimerFreeCase.SetActive(false);
        }

        // Rare and mythical Case

        for (int i = 1; i < 3; i++)
        if ( YandexGame.savesData.Coins < costCase[i])
        {
            buttonsGetCase[i].GetComponent<Button>().enabled = false;
            buttonsGetCase[i].GetComponent<Image>().color = new Color32(255, 255, 255, 150);
        }
        else
        {
            buttonsGetCase[i].GetComponent<Button>().enabled = true;
            buttonsGetCase[i].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
    }

    public void GetFreeReward()
    {
        switch (YandexGame.savesData.FreeRewardIndex)
        {
            case 0:     // Coins
                YandexGame.savesData.Coins += freeRewardValue;
                YandexGame.savesData.FreeRewardIndex++;

                break;

            case 1:     // Food
                YandexGame.savesData.Eat += freeRewardValue;
                YandexGame.savesData.FreeRewardIndex++;
                break;

            case 2:     // Extra Shoots
                YandexGame.savesData.ExtraShoots += freeRewardValue;
                YandexGame.savesData.FreeRewardIndex = 0;
                break;
        }

        YandexGame.savesData.FreeRewardReady = false;
        YandexGame.SaveProgress();
        PlayerStats.playerStats.UpdateStats();
    }

    public void GetSuperOffer()
    {
        YandexGame.savesData.Coins += 1000 + lastOpenLocationIndex * 1000;
        YandexGame.savesData.Eat += 200 + lastOpenLocationIndex * 200;

        YandexGame.savesData.PetsCount[9 + 10 * lastOpenLocationIndex]++ ;

        YandexGame.savesData.SuperOffersBought[lastOpenLocationIndex] = true;
        YandexGame.SaveProgress();

        offerPetImage.sprite = spritesLegendaryPets[lastOpenLocationIndex];

        getPet.SetActive(true);
        getPet.GetComponent<GetPetDailyReward>().StartAnimation();

        PlayerStats.playerStats.UpdateStats();

        legendaryPetSound.PlayOneShot();
    }

    public void OpenCase(int index)
    {
        switch(index)
        {
            case 0:
                YandexGame.savesData.FreeCaseReady = false;
                areaCase.color = Color.white;
                SetCoins(25, 50);
                SetFood(20, 30);
                SetCommonPet(0);
                SetCommonPet(1);
                break;
            case 1:
                YandexGame.savesData.Coins -= costCase[1];
                areaCase.color = Color.green;
                SetCoins(50, 100);
                SetFood(40, 50);
                SetCommonPet(0);
                SetCommonPet(1);
                SetRarePet(2);
                break;
            case 2:
                YandexGame.savesData.Coins -= costCase[2];
                areaCase.color = Color.magenta;
                SetCoins(100, 300);
                SetFood(70, 90);
                SetCommonPet(0);
                SetRarePet(1);
                SetRarePet(2);
                SetMythicalPet(3);
                break;
            case 3:
                areaCase.color = Color.yellow;
                SetCoins(300, 500);
                SetFood(125, 150);
                SetRarePet(0);
                SetMythicalPet(1);
                SetMythicalPet(2);
                SetLegendaryPet(3);
                // Minus donate
                break;
        }


        eggCase.sprite = eggSprites[index];
        openCasePanel.IndexOfCase = index;
        openCasePanel.SetDefaultSettings();
        casePanel.SetActive(true);

        YandexGame.SaveProgress();
    }

    public void OpenInfoCase(int index)
    {
        for(int i = 0; i < nameCases.Length; i++)
        {
            nameCases[i].SetActive(index == i);
        }

        eggImage.sprite = eggSprites[index];
        shadowImage.sprite = eggSprites[index];

        for (int i = 0; i < rewards.Length; i++)
        {
            rewards[i].text = rewardStrings[index, i];
        }

        infoCasePanel.SetActive(true);
    }

    // Voids calculate drop

    private void SetCoins(int minValue, int maxValue)
    {
        int value = UnityEngine.Random.Range(minValue, maxValue);
        YandexGame.savesData.Coins += value;
        coinsDropValue.text = value.ToString();
    }

    private void SetFood(int minValue, int maxValue)
    {
        int value = UnityEngine.Random.Range(minValue, maxValue);
        YandexGame.savesData.Eat += value;
        foodDropValue.text = value.ToString();
    }

    private void SetCommonPet(int index)
    {
        int lastLoc = 0;

        for (int i = 0; i < YandexGame.savesData.OpenLocations.Length; i++)
        {
            if (YandexGame.savesData.OpenLocations[i]) { lastLoc = i + 1; }
        }

        int loc = UnityEngine.Random.Range(0, lastLoc);

        int pet = UnityEngine.Random.Range(0, 6);

        YandexGame.savesData.PetsCount[loc * 10 + pet]++;
        imagesDropPets[index].sprite = petSprites[loc * 10 + pet];
        areasOfDropPets[index].color = Color.white;
    }

    private void SetRarePet(int index)
    {
        int lastLoc = 0;

        for (int i = 0; i < YandexGame.savesData.OpenLocations.Length; i++)
        {
            if (YandexGame.savesData.OpenLocations[i]) { lastLoc = i + 1; }
        }

        int loc = UnityEngine.Random.Range(0, lastLoc);

        int pet = UnityEngine.Random.Range(6, 8);

        YandexGame.savesData.PetsCount[loc * 10 + pet]++;
        imagesDropPets[index].sprite = petSprites[loc * 10 + pet];
        areasOfDropPets[index].color = Color.green;
    }

    private void SetMythicalPet(int index)
    {
        int lastLoc = 0;

        for (int i = 0; i < YandexGame.savesData.OpenLocations.Length; i++)
        {
            if (YandexGame.savesData.OpenLocations[i]) { lastLoc = i + 1; }
        }

        int loc = UnityEngine.Random.Range(0, lastLoc);

        YandexGame.savesData.PetsCount[loc * 10 + 8]++;
        imagesDropPets[index].sprite = petSprites[loc * 10 + 8];
        areasOfDropPets[index].color = Color.magenta;
    }

    private void SetLegendaryPet(int index)
    {
        int lastLoc = 0;

        for (int i = 0; i < YandexGame.savesData.OpenLocations.Length; i++)
        {
            if (YandexGame.savesData.OpenLocations[i]) { lastLoc = i + 1; }
        }

        int loc = UnityEngine.Random.Range(0, lastLoc);

        YandexGame.savesData.PetsCount[loc * 10 + 9]++;
        imagesDropPets[index].sprite = petSprites[loc * 10 + 9];
        areasOfDropPets[index].color = Color.yellow;
    }
}
