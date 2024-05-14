using UnityEngine;
using UnityEngine.UI;
using YG;

public class FightAnimation : MonoBehaviour
{
    // UI Objects

    [SerializeField] private GameObject whiteScreen;
    [SerializeField] private Image whiteScreenImage;
    public GameObject MonsterContainer;
    public GameObject MonsterName;
    [SerializeField] private GameObject[] hideObjects;
    [SerializeField] private GameObject[] whiteRays = new GameObject[5];

    // Get Reward object

    [SerializeField] private Sprite[] pets = new Sprite[10];
    [SerializeField] private Sprite[] simpleRewards = new Sprite[3];
    [SerializeField] private int[] indexOfPets = new int[10];
    [SerializeField] private GameObject[] personalInfo = new GameObject[10];

    [SerializeField] private Image rewardImage, areaImage;
    [SerializeField] private GameObject area, reward, buttonBackWalk, textOfSimpleReward, buttonBackMapInTheEnd;
    [SerializeField] private GameObject bullonWalk, buttonGoSampleScene;

    // process objects

    private float timer = 0;
    public bool StartCrackAnimation = false;
    public int TypeOfReward;
    public int CountOfReward;
    public int[] ChansesPets;
    private int multiplier = 1;
    [SerializeField] private GameObject heartPrefab;
    [SerializeField] private CampainController campainController;

    [SerializeField] private MonstersLearning monstersLearning;

    [SerializeField] private PlayClip saspens, getCommonPet, getRarePet, getMythicPet, getLegendaryPet, getReward;

    private void FixedUpdate()
    {
        if (StartCrackAnimation)
        {
            timer += Time.deltaTime;

            if (timer <= 4)
            {
                MonsterContainer.transform.position += new Vector3(multiplier / 10f, 0, 0);

                if (MonsterContainer.transform.position.x >= 0.075) { multiplier = -1; }
                if (MonsterContainer.transform.position.x <= -0.075) { multiplier = 1; }
            }

            if (timer >= 3 && timer < 4)
            {
                whiteScreenImage.color += new Color32(0, 0, 0, 6);
            }

            if (timer >= 4 && timer < 5)
            {
                whiteScreenImage.color -= new Color32(0, 0, 0, 6);
            }

            if (timer >= 4)
            {
                foreach (GameObject ray in whiteRays)
                {
                    ray.SetActive(false);
                }
            }
            else
            {
                if (timer > 0.5) { whiteRays[0].SetActive(true); }
                if (timer > 1) { whiteRays[1].SetActive(true); }
                if (timer > 1.5) { whiteRays[2].SetActive(true); }
                if (timer > 2) { whiteRays[3].SetActive(true); }
                if (timer > 2.5) { whiteRays[4].SetActive(true); }
            }
        }
    }

    public void MonsterIsDefeat()
    {
        StartCrackAnimation = true;
        whiteScreen.SetActive(true);
        saspens.PlayOneShot();
        Invoke("OpenMonster", 4);
        Invoke("EndOpenAnimation", 5);
    }

    private void OpenMonster()
    {
        MonsterContainer.SetActive(false);
        MonsterName.SetActive(false);
        MonsterContainer.transform.position = new Vector3(0, 0, 0);
        foreach (GameObject hideObject in hideObjects)
        {
            hideObject.SetActive(false);
        }

        switch (TypeOfReward)
        {
            // Exp
            case 0:
                YandexGame.savesData.Exp += CountOfReward;
                YandexGame.SaveProgress();
                GetSimpleReward();
                break;

            // Coins
            case 1:
                YandexGame.savesData.Coins += CountOfReward;
                YandexGame.SaveProgress();
                GetSimpleReward();
                break;

            // Food
            case 2:
                YandexGame.savesData.Eat += CountOfReward;
                YandexGame.SaveProgress();
                GetSimpleReward();
                break;

            // Pet
            case 3:
                GetPet();
                Instantiate(heartPrefab);
                campainController.UpdateCompletedlevels();
                break;
        }

        PlayerStats.playerStats.UpdateStats();
    }

    private void EndOpenAnimation()
    {
        whiteScreen.SetActive(false);
        StartCrackAnimation = false;
        timer = 0;
    }

    private void GetPet()
    {
        int randomNum = Random.Range(0, 100);

        if (YandexGame.savesData.CurrentLearningStep == 59) { YandexGame.savesData.CurrentLearningStep++;}

        if (randomNum < ChansesPets[0])
        {
            area.SetActive(true);
            reward.SetActive(true);
            buttonBackMapInTheEnd.SetActive(true);
            areaImage.color = Color.white;
            rewardImage.rectTransform.sizeDelta = new Vector2(500, 500);
            int i = Random.Range(0, 6);
            if (YandexGame.savesData.CurrentLearningStep < 63) { i = 1; }
            rewardImage.sprite = pets[i];
            personalInfo[i].SetActive(true);
            YandexGame.savesData.PetsCount[indexOfPets[i]]++;
            YandexGame.SaveProgress();
            getCommonPet.PlayOneShot();
        }
        else if (randomNum < ChansesPets[1])
        {
            area.SetActive(true);
            reward.SetActive(true);
            buttonBackMapInTheEnd.SetActive(true);
            areaImage.color = Color.green;
            rewardImage.rectTransform.sizeDelta = new Vector2(700, 700);
            int j = Random.Range(6, 8);
            rewardImage.sprite = pets[j];
            personalInfo[j].SetActive(true);
            YandexGame.savesData.PetsCount[indexOfPets[j]]++;
            YandexGame.SaveProgress();
            getRarePet.PlayOneShot();
        }
        else if (randomNum < ChansesPets[2])
        {
            area.SetActive(true);
            reward.SetActive(true);
            buttonBackMapInTheEnd.SetActive(true);
            areaImage.color = new Color32(255, 0, 255, 255);
            rewardImage.rectTransform.sizeDelta = new Vector2(850, 850);
            rewardImage.sprite = pets[8];
            personalInfo[8].SetActive(true);
            YandexGame.savesData.PetsCount[indexOfPets[8]]++;
            YandexGame.SaveProgress();
            getMythicPet.PlayOneShot();
        }
        else if (randomNum < ChansesPets[3])
        {
            area.SetActive(true);
            reward.SetActive(true);
            buttonBackMapInTheEnd.SetActive(true);
            areaImage.color = new Color32(255, 255, 0, 255);
            rewardImage.rectTransform.sizeDelta = new Vector2(1000, 1000);
            rewardImage.sprite = pets[9];
            personalInfo[9].SetActive(true);
            YandexGame.savesData.PetsCount[indexOfPets[9]]++;
            YandexGame.SaveProgress();
            getLegendaryPet.PlayOneShot();
        }
    }

    public void GetSimpleReward()
    {
        rewardImage.sprite = simpleRewards[TypeOfReward];
        textOfSimpleReward.SetActive(true);
        textOfSimpleReward.GetComponent<Text>().text = CountOfReward.ToString();

        area.SetActive(true);
        reward.SetActive(true);
        buttonBackWalk.SetActive(true);
        areaImage.color = Color.white;
        rewardImage.rectTransform.sizeDelta = new Vector2(200, 200);
        getReward.PlayOneShot();
    }

    public void CloseRewardComponents()
    {
        buttonBackWalk.SetActive(false);
        reward.SetActive(false);
        area.SetActive(false);
        textOfSimpleReward.SetActive(false);
        foreach (GameObject petInfo in personalInfo)
        {
            petInfo.SetActive(false);
        }

        if (YandexGame.savesData.CurrentLearningStep >= 63) { bullonWalk.SetActive(true); }
        if (YandexGame.savesData.CurrentLearningStep >= 63) { buttonGoSampleScene.SetActive(true); }
        campainController.CurrentStep++;
        campainController.UpdateStepUI();

        if (YandexGame.savesData.CurrentLearningStep < 63) { monstersLearning.SetStepSettings(); }
    }
}
