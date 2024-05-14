using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class BuyEatPanel : MonoBehaviour
{
    [SerializeField] private GameObject buyEatPanel;
    [SerializeField] private string shopScene;
    [SerializeField] private PetSceneController petSceneController;
    [SerializeField] private int firstCost, secondCost;
    [SerializeField] private Image firstButton, secondButton;
    [SerializeField] private int firstEatCount, secondEatCount;

    private void Update()
    {
        UpdateButtonView(firstCost, firstButton);
        UpdateButtonView(secondCost, secondButton);
    }

    private void UpdateButtonView(int cost, Image button)
    {
        if (YandexGame.savesData.Coins < cost)
        {
            button.color = new Color32(150, 150, 150, 255);
        }
        else
        {
            button.color = new Color32(255, 255, 255, 255);
        }
    }

    public void BuyFoodPerIndex(int index)
    {
        if (index == 0) { BuyFood(firstCost, firstEatCount); }
        if (index == 1) { BuyFood(secondCost, secondEatCount); }
    }

    private void BuyFood(int cost, int eatCount)
    {
        if (YandexGame.savesData.Coins >= cost)
        {
            YandexGame.savesData.Coins -= cost;
            YandexGame.savesData.Eat += eatCount;
            YandexGame.SaveProgress();

            buyEatPanel.SetActive(false);
            petSceneController.UpdateInfoPanel(petSceneController.indexOfOpenPet);
        }
        else
        {
            SceneManager.LoadScene(shopScene);
        }
    }

    public void ClosePanel()
    {
        buyEatPanel.SetActive(false);
    }
}
