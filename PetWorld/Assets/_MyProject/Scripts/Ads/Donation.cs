using UnityEngine;
using YG;

public class Donation : MonoBehaviour
{
    [SerializeField] private ShopSceneController shopSceneController;

    private void OnEnable()
    {
        YandexGame.PurchaseSuccessEvent += DonateReward;
    }

    private void OnDisable()
    {
        YandexGame.PurchaseSuccessEvent -= DonateReward;
    }

    private void DonateReward(string key)
    {
        switch (key)
        {
            case "Offer":
                shopSceneController.GetSuperOffer();
                break;
            case "Egg":
                shopSceneController.OpenCase(3);
                break;
            case "MoneySmall":
                YandexGame.savesData.Coins += 500;
                break;
            case "MoneyMiddle":
                YandexGame.savesData.Coins += 3000;
                break;
            case "MoneyHuge":
                YandexGame.savesData.Coins += 10000;
                break;
        }

        YandexGame.SaveProgress();
        PlayerStats.playerStats.UpdateStats();
    }
}
