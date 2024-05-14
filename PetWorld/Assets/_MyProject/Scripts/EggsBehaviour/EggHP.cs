using UnityEngine;
using UnityEngine.UI;
using YG;

public class EggHP : MonoBehaviour
{
    // Info

    public int Health, CurrentHealth;
    private bool rewardGot = false;

    // UI

    [SerializeField] private string eggType;
    [SerializeField] private Image lifeLine;
    [SerializeField] private Text textHP;
    [SerializeField] private TapOnEgg tapOnEgg;
    [SerializeField] private CrackAnimation crackAnimation;
    [SerializeField] private World1Learning world1Learning;

    private void Update()
    {
        if (YandexGame.savesData.CurrentLearningStep == 30 && CurrentHealth <= 100) { world1Learning.SetStepSettings(); }

        if (gameObject.activeSelf && CurrentHealth > 0)
        {
            lifeLine.fillAmount = (float)CurrentHealth / Health;

            textHP.text = CurrentHealth.ToString();

            rewardGot = false;
        }
        else
        {
            if (! rewardGot)
            {
                rewardGot = true;
                tapOnEgg.eggHasHP = false;
                CurrentHealth = 0;
                lifeLine.fillAmount = (float)CurrentHealth / Health;
                textHP.text = CurrentHealth.ToString();

                crackAnimation.eggType = eggType;
                crackAnimation.EggIsCrack();
            }
        }
    }
}
