using UnityEngine;
using UnityEngine.UI;
using YG;

public class WalkCampain : MonoBehaviour
{
    // UI objects

    [SerializeField] private GameObject searchPanel;
    [SerializeField] private GameObject buttonWalk;
    [SerializeField] private GameObject getExp, getCoins, getFood;
    [SerializeField] private int expReward, coinsReward, foodReward;

    // Scene objects

    [SerializeField] private CampainController campainController;
    [SerializeField] private int costOfWalk;
    [SerializeField] private GameObject buyEnergyPanel;

    [SerializeField] private MonstersLearning monstersLearning;
    [SerializeField] private PlayClip getReward;

    [SerializeField] private GameObject plusButton;

    public void GoWalk()
    {
        if (YandexGame.savesData.CurrentLearningStep == 48)
        {
            monstersLearning.SetStepSettings();
        }

        if (YandexGame.savesData.Energy >= costOfWalk)
        {
            buttonWalk.SetActive(false);
            searchPanel.SetActive(true);

            YandexGame.savesData.Energy -= costOfWalk;
            YandexGame.SaveProgress();

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

        if (YandexGame.savesData.CurrentLearningStep < 63) { randomReward = 4; }

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
                if (YandexGame.savesData.CurrentLearningStep == 49) { monstersLearning.SetStepSettings(); }
                campainController.SpawnNextStep();
                break;
        }
    }
}
