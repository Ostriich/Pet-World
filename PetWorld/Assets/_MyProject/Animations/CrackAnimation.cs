using UnityEngine;
using UnityEngine.UI;
using YG;

public class CrackAnimation : MonoBehaviour
{
    // UI Objects

    [SerializeField] private GameObject whiteScreen;
    [SerializeField] private Image whiteScreenImage;
    public GameObject EggContainer;
    [SerializeField] private GameObject[] hideObjects;
    [SerializeField] private GameObject[] whiteRays = new GameObject[5];

    // Get Reward object

    [SerializeField] private Sprite[] pets = new Sprite[10];
    [SerializeField] private int[] indexOfPets = new int[10];
    [SerializeField] private GameObject[] personalInfo = new GameObject[10];

    [SerializeField] private Image petImage, areaImage;
    [SerializeField] private GameObject area, pet, buttonBackWalk, buttonGoPetsScene;
    [SerializeField] private GameObject bullonWalk, buttonGoSampleScene;

    // process objects

    private float timer = 0;
    public bool StartCrackAnimation = false;
    private int multiplier = 1;
    public string eggType;
    [SerializeField] private EggController eggController;
    [SerializeField] private RareEggAnimation rareEggAnimation;
    [SerializeField] private GameObject heartPrefab;
    [SerializeField] private World1Learning world1Learning;

    [SerializeField] private PlayClip saspens, getCommonPet, getRarePet, getMythicPet, getLegendaryPet, dangerSound;

    private void FixedUpdate()
    {
        if (StartCrackAnimation)
        {
            timer += Time.deltaTime;

            if (timer <= 4)
            {
                EggContainer.transform.position += new Vector3(multiplier / 10f, 0, 0);

                if (EggContainer.transform.position.x >= 0.075) { multiplier = -1; }
                if (EggContainer.transform.position.x <= -0.075) { multiplier = 1; }
            }

            if (timer >= 3 && timer < 4)
            {
                whiteScreenImage.color += new Color32(0, 0, 0, 6);
            }

            if (timer >= 4 && timer < 5)
            {
                whiteScreenImage.color -= new Color32(0, 0, 0, 6);
            }

            if (timer>=4)
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

    public void EggIsCrack()
    {
        StartCrackAnimation = true;
        saspens.PlayOneShot();
        whiteScreen.SetActive(true);
        Invoke("OpenEgg", 4);
        Invoke("EndOpenAnimation", 5);
    }
    
    private void OpenEgg()
    {
        if (YandexGame.savesData.CurrentLearningStep ==32) { world1Learning.SetStepSettings(); }

        EggContainer.SetActive(false);
        EggContainer.transform.position = new Vector3(0, 0, 0);
        foreach (GameObject hideObject in hideObjects)
        {
            hideObject.SetActive(false);
        }

        if (eggType == "common") { GetPetOutCommonEgg(); }
        else 
        { 
            GetPetOutRareEgg();
            Instantiate(heartPrefab);
        }

        PlayerStats.playerStats.UpdateStats();
    }

    private void EndOpenAnimation()
    {
        whiteScreen.SetActive(false);
        StartCrackAnimation = false;
        timer = 0;
    }

    private void GetPetOutCommonEgg()
    {
        int randomNum = Random.Range(0, 100);
        if (YandexGame.savesData.CurrentLearningStep ==33) { randomNum = 50; }

        switch (randomNum)
        {
            case < 20:
                eggController.SpawnRareEgg();
                rareEggAnimation.StartAnimation();
                dangerSound.PlayOneShot();
                break;
            case < 84:
                area.SetActive(true);
                pet.SetActive(true);
                if (YandexGame.savesData.CurrentLearningStep != 33) buttonBackWalk.SetActive(true);
                if (YandexGame.savesData.CurrentLearningStep != 33) buttonGoPetsScene.SetActive(true);
                areaImage.color = Color.white;
                petImage.rectTransform.sizeDelta = new Vector2(500, 500);
                int i = Random.Range(0, 6);
                if (YandexGame.savesData.CurrentLearningStep == 33) { i = 5; }
                petImage.sprite = pets[i];
                personalInfo[i].SetActive(true);
                YandexGame.savesData.PetsCount[indexOfPets[i]]++;
                YandexGame.SaveProgress();
                Instantiate(heartPrefab);
                getCommonPet.PlayOneShot();
                break;
            case < 96:
                area.SetActive(true);
                pet.SetActive(true);
                buttonBackWalk.SetActive(true);
                buttonGoPetsScene.SetActive(true);
                areaImage.color = Color.green;
                petImage.rectTransform.sizeDelta = new Vector2(700, 700);
                int j = Random.Range(6, 8);
                petImage.sprite = pets[j];
                personalInfo[j].SetActive(true);
                YandexGame.savesData.PetsCount[indexOfPets[j]]++;
                YandexGame.SaveProgress();
                Instantiate(heartPrefab);
                getRarePet.PlayOneShot();
                break;
            case < 100:
                area.SetActive(true);
                pet.SetActive(true);
                buttonBackWalk.SetActive(true);
                buttonGoPetsScene.SetActive(true);
                areaImage.color = new Color32(255, 0, 255, 255);
                petImage.rectTransform.sizeDelta = new Vector2(850, 850);
                petImage.sprite = pets[8];
                personalInfo[8].SetActive(true);
                YandexGame.savesData.PetsCount[indexOfPets[8]]++;
                YandexGame.SaveProgress();
                Instantiate(heartPrefab);
                getMythicPet.PlayOneShot();
                break;
        }
    }

    private void GetPetOutRareEgg()
    {
        int randomNum = Random.Range(0, 100);
        switch (randomNum)
        {
            case < 80:
                area.SetActive(true);
                pet.SetActive(true);
                buttonBackWalk.SetActive(true);
                buttonGoPetsScene.SetActive(true);
                areaImage.color = Color.green;
                petImage.rectTransform.sizeDelta = new Vector2(700, 700);
                int i = Random.Range(6, 8);
                petImage.sprite = pets[i];
                personalInfo[i].SetActive(true);
                YandexGame.savesData.PetsCount[indexOfPets[i]]++;
                YandexGame.SaveProgress();
                getRarePet.PlayOneShot();
                break;
            case < 95:
                area.SetActive(true);
                pet.SetActive(true);
                buttonBackWalk.SetActive(true);
                buttonGoPetsScene.SetActive(true);
                areaImage.color = new Color32(255, 0, 255, 255);
                petImage.rectTransform.sizeDelta = new Vector2(850, 850);
                petImage.sprite = pets[8];
                personalInfo[8].SetActive(true);
                YandexGame.savesData.PetsCount[indexOfPets[8]]++;
                YandexGame.SaveProgress();
                getMythicPet.PlayOneShot();
                break;
            case < 100:
                area.SetActive(true);
                pet.SetActive(true);
                buttonBackWalk.SetActive(true);
                buttonGoPetsScene.SetActive(true);
                areaImage.color = Color.yellow;
                petImage.rectTransform.sizeDelta = new Vector2(1000, 1000);
                petImage.sprite = pets[9];
                personalInfo[9].SetActive(true);
                YandexGame.savesData.PetsCount[indexOfPets[9]]++;
                YandexGame.SaveProgress();
                getLegendaryPet.PlayOneShot();
                break;
        }
    }

    public void CloseRewardComponents()
    {
        buttonGoPetsScene.SetActive(false);
        buttonBackWalk.SetActive(false);
        pet.SetActive(false);
        area.SetActive(false);
        foreach (GameObject petInfo in personalInfo)
        {
            petInfo.SetActive(false);
        }

        bullonWalk.SetActive(true);
        buttonGoSampleScene.SetActive(true);
    }
}
