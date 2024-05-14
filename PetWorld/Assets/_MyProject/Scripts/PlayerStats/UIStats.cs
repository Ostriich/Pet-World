using System;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class UIStats : MonoBehaviour
{
    // UI Stats

    [SerializeField] private Text healthText, attackText, coinsText, energyText, levelText, expText, loadEnergyText;
    [SerializeField] private Image expImage, healthImage;
    [SerializeField] private PlayClip newLevelSound;


    private void Start()
    {
        PlayerStats.playerStats.UpdateStats();
    }


    private void Update()
    {
        healthText.text = ShortCutValue.shortCutValue.CutIntValue(PlayerStats.playerStats.CurrentHealthPlayer, 2);
        if (PlayerStats.playerStats.HealthPlayer != 0) { healthImage.fillAmount = (float)PlayerStats.playerStats.CurrentHealthPlayer / PlayerStats.playerStats.HealthPlayer; }
        attackText.text = ShortCutValue.shortCutValue.CutIntValue(PlayerStats.playerStats.AttackPlayer, 2);

        coinsText.text = ShortCutValue.shortCutValue.CutIntValue(YandexGame.savesData.Coins, 1);
        levelText.text = YandexGame.savesData.Level.ToString();
        energyText.text = YandexGame.savesData.Energy.ToString() + "/" + (10 + (YandexGame.savesData.Level / 5) * 5).ToString();
        if (Mathf.Round(PlayerStats.playerStats.TimeLoadEnergy - YandexGame.savesData.TimerEnergy) != PlayerStats.playerStats.TimeLoadEnergy)
            loadEnergyText.text = Mathf.Round(PlayerStats.playerStats.TimeLoadEnergy - YandexGame.savesData.TimerEnergy).ToString();
        else
            loadEnergyText.text = "";
        expText.text = ShortCutValue.shortCutValue.CutIntValue(YandexGame.savesData.Exp,1) + "/" + ShortCutValue.shortCutValue.CutIntValue(YandexGame.savesData.Level * 100, 1);
        expImage.fillAmount = (float)YandexGame.savesData.Exp / (YandexGame.savesData.Level * 100);

        // Check new Level

        if (YandexGame.savesData.Exp >= (YandexGame.savesData.Level * 100))
        {
            YandexGame.savesData.Exp -= (YandexGame.savesData.Level * 100);
            YandexGame.savesData.Level++;
            YandexGame.savesData.Energy = 10 + YandexGame.savesData.Level / 5 * 5;
            YandexGame.SaveProgress();

            newLevelSound.PlayOneShot();
            GetComponent<NewLevelAnimation>().StartAnimation();
        }
    }
}
