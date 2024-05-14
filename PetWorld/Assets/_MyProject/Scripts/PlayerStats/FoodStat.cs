using UnityEngine;
using UnityEngine.UI;
using YG;

public class FoodStat : MonoBehaviour
{
    [SerializeField] private Text foodValue;

    private void Update()
    {
        foodValue.text = YandexGame.savesData.Eat.ToString();
    }
}
