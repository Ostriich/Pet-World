using UnityEngine;
using UnityEngine.UI;
using YG;

public class PetSceneController : MonoBehaviour
{
    // Panels

    [SerializeField] private GameObject panelAllPets, panelPetInfo;

    // UI Objects

    [SerializeField] private GameObject[] textLocked;
    [SerializeField] private GameObject[] namesOpenedWorlds;
    [SerializeField] private Button[] petsButtons;
    [SerializeField] private Image[] petsImages;

    [SerializeField] private Image petInfoImage;
    [SerializeField] private Sprite[] petsImagesInfoPanel;
    [SerializeField] private GameObject[] personalBlocks;
    [SerializeField] private Text hpText, damageText, levelPetText, countText, feedText, costSellText;
    [SerializeField] private GameObject[] rarePet = new GameObject[4];
    [SerializeField] private GameObject statPet, levelContainer;

    // Technical Objects
    public int indexOfOpenPet;
    [SerializeField] private GameObject buttonFeed, buttonSell;
    [SerializeField] private Text buttonFeedText, buttonSellText;
    [SerializeField] private GameObject leftArrow, rightArrow;
    [SerializeField] private GameObject feedPrefab;
    [SerializeField] private GameObject buyFoodPanel;
    [SerializeField] private GameObject contentPos;

    [SerializeField] private PetsLearning petsLearning;
    [SerializeField] private PlayClip feedSound, buttonSound;

    private void Start()
    {
        UpdateAllPetsPanel();
        if (YandexGame.savesData.CurrentLearningStep >= 63) { contentPos.transform.localPosition = new Vector3(0, -1900, 0); }
        else { contentPos.transform.localPosition = new Vector3(0, -1400, 0); }
    }

    public void OpenPetInfoPanel(int petIndex)
    {
        if (YandexGame.savesData.CurrentLearningStep == 37) { petsLearning.SetStepSettings(); }

        UpdateInfoPanel(petIndex);
        indexOfOpenPet = petIndex;

        panelPetInfo.SetActive(true);
        panelAllPets.SetActive(false);
    }

    public void UpdateInfoPanel(int petIndex)
    {
        for (int i = 0; i < personalBlocks.Length; i++)
        {
            if (petIndex != i)
            {
                personalBlocks[i].SetActive(false);
            }
            else
            {
                if (YandexGame.savesData.CurrentLearningStep >= 63)
                {
                    buttonFeed.SetActive(true);
                    buttonSell.SetActive(true);
                }
                petInfoImage.sprite = petsImagesInfoPanel[i];
                countText.text = "x" + YandexGame.savesData.PetsCount[i].ToString();
                if (YandexGame.savesData.PetsCount[i] != 0) 
                {
                    statPet.SetActive(true);
                    levelContainer.SetActive(true);
                    personalBlocks[i].SetActive(true);
                    petInfoImage.color = Color.white;
                    levelPetText.text = (YandexGame.savesData.PetsLevels[i] + 1).ToString();

                    if (YandexGame.savesData.Eat >= 10 + YandexGame.savesData.PetsLevels[i] * 10)
                    {
                        buttonFeed.GetComponent<Image>().color = Color.white;
                        buttonFeedText.color = new Color32(100, 75, 50, 255);
                    }
                    else
                    {
                        buttonFeed.GetComponent<Image>().color = new Color32(255,255,255,150);
                        buttonFeedText.color = new Color32(100, 75, 50, 150);
                    }

                    if (YandexGame.savesData.PetsCount[i] > 1)
                    {
                        buttonSell.GetComponent<Button>().enabled = true;
                        buttonSell.GetComponent<Image>().color = Color.white;
                        buttonSellText.color = new Color32(100, 75, 50, 255);
                    }
                    else
                    {
                        buttonSell.GetComponent<Button>().enabled = false;
                        buttonSell.GetComponent<Image>().color = new Color32(255, 255, 255, 150);
                        buttonSellText.color = new Color32(100, 75, 50, 150);
                    }

                    feedText.text = (10 +YandexGame.savesData.PetsLevels[i] * 10).ToString();   
                }
                else 
                { 
                    foreach (GameObject info in personalBlocks)
                    {
                        info.SetActive(false);
                    }
                    statPet.SetActive(false);
                    levelContainer.SetActive(false);
                    petInfoImage.color = Color.black;
                    levelPetText.text = "1";
                    buttonFeed.SetActive(false);
                    buttonSell.SetActive(false);
                }
                switch (i % 10)
                {
                    case < 6:
                        damageText.text = (75 + YandexGame.savesData.PetsLevels[i] * 5).ToString();
                        hpText.text = (50 + YandexGame.savesData.PetsLevels[i] * 10).ToString();
                        costSellText.text = "50";
                        petInfoImage.rectTransform.sizeDelta = new Vector2(400, 400);
                        if (YandexGame.savesData.PetsLevels[i] == 4)
                        {
                            feedText.text = "";
                            buttonFeed.SetActive(false);
                        }
                        rarePet[0].SetActive(YandexGame.savesData.PetsCount[i] != 0);
                        rarePet[1].SetActive(false);
                        rarePet[2].SetActive(false);
                        rarePet[3].SetActive(false);
                        break;
                    case < 8:
                        damageText.text = (100 + YandexGame.savesData.PetsLevels[i] * 7).ToString();
                        hpText.text = (100 + YandexGame.savesData.PetsLevels[i] * 5).ToString();
                        costSellText.text = "100";
                        petInfoImage.rectTransform.sizeDelta = new Vector2(550, 550);
                        if (YandexGame.savesData.PetsLevels[i] == 9)
                        {
                            feedText.text = "";
                            buttonFeed.SetActive(false);
                        }
                        rarePet[0].SetActive(false);
                        rarePet[1].SetActive(YandexGame.savesData.PetsCount[i] != 0);
                        rarePet[2].SetActive(false);
                        rarePet[3].SetActive(false);
                        break;
                    case 8:
                        damageText.text = (150 + YandexGame.savesData.PetsLevels[i] * 10).ToString();
                        hpText.text = (200 + YandexGame.savesData.PetsLevels[i] * 12).ToString();
                        costSellText.text = "250";
                        petInfoImage.rectTransform.sizeDelta = new Vector2(700, 700);
                        if (YandexGame.savesData.PetsLevels[i] == 14)
                        {
                            feedText.text = "";
                            buttonFeed.SetActive(false);
                        }
                        rarePet[0].SetActive(false);
                        rarePet[1].SetActive(false);
                        rarePet[2].SetActive(YandexGame.savesData.PetsCount[i] != 0);
                        rarePet[3].SetActive(false);
                        break;
                    case 9:
                        damageText.text = (200 + YandexGame.savesData.PetsLevels[i] * 15).ToString();
                        hpText.text = (300 + YandexGame.savesData.PetsLevels[i] * 15).ToString();
                        costSellText.text = "500";
                        petInfoImage.rectTransform.sizeDelta = new Vector2(1000, 1000);
                        if (YandexGame.savesData.PetsLevels[i] == 19)
                        {
                            feedText.text = "";
                            buttonFeed.SetActive(false);
                        }
                        rarePet[0].SetActive(false);
                        rarePet[1].SetActive(false);
                        rarePet[2].SetActive(false);
                        rarePet[3].SetActive(YandexGame.savesData.PetsCount[i] != 0);
                        break;
                }
            }

            if (YandexGame.savesData.CurrentLearningStep >= 63) { leftArrow.SetActive(petIndex != 0); }

            if (petIndex != petsImagesInfoPanel.Length - 1)
            {
                if (YandexGame.savesData.CurrentLearningStep >= 63)
                if (YandexGame.savesData.OpenLocations[(petIndex + 1) / 10]) { rightArrow.SetActive(true); }
                else { rightArrow.SetActive(false); }
            }
            else { rightArrow.SetActive(false); }
        }
    }

    public void UpdateAllPetsPanel()
    {
        for (int i = 0; i < petsImages.Length; i++)
        {
            if (YandexGame.savesData.PetsCount[i] > 0)
            {
                petsImages[i].color = Color.white;
            }
            else
            {
                petsImages[i].color = Color.black;
            }

            if (YandexGame.savesData.CurrentLearningStep >= 63)petsButtons[i].enabled = YandexGame.savesData.OpenLocations[i / 10];
        }

        for (int i = 0; i < textLocked.Length; i++) 
        { 
            textLocked[i].SetActive( !YandexGame.savesData.OpenLocations[i] );
            namesOpenedWorlds[i].SetActive( YandexGame.savesData.OpenLocations[i] );
        }
    }

    public void ClosePetInfoPanel()
    {
        if (YandexGame.savesData.CurrentLearningStep == 44) { petsLearning.SetStepSettings(); }

        panelPetInfo.SetActive(false);
        panelAllPets.SetActive(true);
    }

    public void FeedPet()
    {
        if ( YandexGame.savesData.Eat >= int.Parse(feedText.text))
        {
            YandexGame.savesData.Eat -= int.Parse(feedText.text);
            YandexGame.savesData.PetsLevels[indexOfOpenPet]++;
            YandexGame.SaveProgress();
            UpdateInfoPanel(indexOfOpenPet);
            PlayerStats.playerStats.UpdateStats();
            Instantiate(feedPrefab);

            if (YandexGame.savesData.CurrentLearningStep == 42 && YandexGame.savesData.PetsLevels[indexOfOpenPet] == 4) { petsLearning.SetStepSettings(); }
            feedSound.PlayOneShot();
        }
        else
        {
            buyFoodPanel.SetActive(true);
            buttonSound.PlayOneShot();
        }
    }

    public void SellPet()
    {
        YandexGame.savesData.PetsCount[indexOfOpenPet]--;
        YandexGame.savesData.Coins += int.Parse(costSellText.text);
        UpdateInfoPanel(indexOfOpenPet);
        PlayerStats.playerStats.UpdateStats();
    }

    public void ArrowClick(int delta)
    {
        UpdateInfoPanel(indexOfOpenPet + delta);
        indexOfOpenPet = indexOfOpenPet + delta;
    }
}
