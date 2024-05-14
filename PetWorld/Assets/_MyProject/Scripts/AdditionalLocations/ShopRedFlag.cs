using UnityEngine;
using YG;

public class ShopRedFlag : MonoBehaviour
{
    [SerializeField] private GameObject redFlag;

    private void Update()
    {
        redFlag.SetActive(YandexGame.savesData.FreeRewardReady || YandexGame.savesData.FreeCaseReady);
    }
}
