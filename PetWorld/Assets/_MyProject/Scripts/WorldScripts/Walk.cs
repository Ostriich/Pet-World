using UnityEngine;
using UnityEngine.UI;
using YG;

public class Walk : MonoBehaviour
{
    // UI objects

    [SerializeField] private GameObject searchPanel;
    [SerializeField] private GameObject buttonWalk;
    [SerializeField] private GameObject getExp, getCoins, getFood;
    [SerializeField] private int expReward, coinsReward, foodReward;

    // Scene objects

    [SerializeField] private EggController eggController;
    [SerializeField] private int costOfWalk;
    [SerializeField] private GameObject buyEnergyPanel;

    [SerializeField] private World1Learning world1Learning;
    [SerializeField] private PlayClip getReward;

    [SerializeField] private GameObject plusButton;

    public void GoWalk()
    {
        if (YandexGame.savesData.Energy >= costOfWalk)
        {
            buttonWalk.SetActive(false);
            searchPanel.SetActive(true);

            YandexGame.savesData.Energy -= costOfWalk;
            YandexGame.SaveProgress();

            if (YandexGame.savesData.CurrentLearningStep == 27)
            {
                world1Learning.SetStepSettings();
            }

            Invoke("GetResultOfWalk", 2);
        }
        else
        {
            buyEnergyPanel.SetActive(true);
        }
    }

    private void Update()
    {
        if (YandexGame.savesData.Energy < costOfWalk) 
        { 
            buttonWalk.GetComponent<Image>().color = new Color32(255, 255, 255, 150);
            plusButton.SetActive(true);
        }
        else 
        { 
            buttonWalk.GetComponent<Image>().color = Color.white;
            plusButton.SetActive(false);
        }
    }

    private void GetResultOfWalk()
    {
        buttonWalk.SetActive(true);
        searchPanel.SetActive(false);

        int randomReward = Random.Range(0, 5);

        if (YandexGame.savesData.CurrentLearningStep == 28) 
        { 
            randomReward = 5;
            world1Learning.SetStepSettings();
        }

        switch (randomReward)
        {
            case 0:
                var prefab = Instantiate(getExp);
                prefab.GetComponent<InstantiateValuePrefab>().Value = expReward;
                prefab.GetComponent<InstantiateValuePrefab>().UpdateValue();
                YandexGame.savesData.Exp += expReward;
                YandexGame.SaveProgress();

                getReward.PlayOneShot();
                break;

            case 1:
                var prefab1 = Instantiate(getCoins);
                prefab1.GetComponent<InstantiateValuePrefab>().Value = coinsReward;
                prefab1.GetComponent<InstantiateValuePrefab>().UpdateValue();
                YandexGame.savesData.Coins += coinsReward;
                YandexGame.SaveProgress();

                getReward.PlayOneShot();
                break;

            case 2:
                var prefab2 = Instantiate(getFood);
                prefab2.GetComponent<InstantiateValuePrefab>().Value = foodReward;
                prefab2.GetComponent<InstantiateValuePrefab>().UpdateValue();
                YandexGame.savesData.Eat += foodReward;
                YandexGame.SaveProgress();

                getReward.PlayOneShot();
                break;

            default:
                eggController.SpawnCommonEgg();
                break;
        }
    }
}
