using UnityEngine;
using UnityEngine.UI;
using YG;
using UnityEngine.SceneManagement;

public class BuyEnergyPanel : MonoBehaviour
{
    [SerializeField] private GameObject buyEnergyPanel;
    [SerializeField] private string shopScene;
    [SerializeField] private Text costValue;

    [SerializeField] private bool isPeacefulWorld;
    [SerializeField] private NewLevelAnimation notEnoughMoney;

    private void Update()
    {
        costValue.text = ShortCutValue.shortCutValue.CutIntValue((10 + YandexGame.savesData.Level / 5 * 5 - YandexGame.savesData.Energy) * 10, 1);
    }

    public void BuyEnergy()
    {
        if (YandexGame.savesData.Coins >= (10 + YandexGame.savesData.Level / 5 * 5 - YandexGame.savesData.Energy) * 10)
        {
            YandexGame.savesData.Coins -= (10 + YandexGame.savesData.Level / 5 * 5 - YandexGame.savesData.Energy) * 10;
            YandexGame.savesData.Energy = 10 + YandexGame.savesData.Level / 5 * 5;
            YandexGame.SaveProgress();

            buyEnergyPanel.SetActive(false);
        }
        else
        {
            if (isPeacefulWorld)
            {
                SceneManager.LoadScene(shopScene);
            }    
            else
            {
                notEnoughMoney.StartAnimation();
                buyEnergyPanel.SetActive(false);
            }
        }
    }

    public void ClosePanel()
    {
        buyEnergyPanel.SetActive(false);
    }
}
