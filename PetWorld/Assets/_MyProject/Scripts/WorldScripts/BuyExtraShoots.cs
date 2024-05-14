using UnityEngine;
using YG;

public class BuyExtraShoots : MonoBehaviour
{

    [SerializeField] private GameObject buyExtraShootsPanel;
    [SerializeField] private string shopScene;
    [SerializeField] private NewLevelAnimation notEnoughMoney;

    public void BuyShoots()
    {
        if (YandexGame.savesData.Coins >= 1000)
        {
            YandexGame.savesData.Coins -= 1000;
            YandexGame.savesData.ExtraShoots = 10;
            YandexGame.SaveProgress();

            buyExtraShootsPanel.SetActive(false);
        }
        else
        {
            notEnoughMoney.StartAnimation();
            buyExtraShootsPanel.SetActive(false);
        }
    }

    public void ClosePanel()
    {
        buyExtraShootsPanel.SetActive(false);
    }
}
