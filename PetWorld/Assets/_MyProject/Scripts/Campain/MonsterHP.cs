using UnityEngine;
using UnityEngine.UI;

public class MonsterHP : MonoBehaviour
{
    public int Health, CurrentHealth;
    private bool rewardGot = false;

    // UI

    [SerializeField] private Image lifeLine;
    [SerializeField] private Text textHP;
    [SerializeField] private TapOnMonster tapOnMonster;
    [SerializeField] private FightAnimation fightAnimation;
    [SerializeField] private MonsterAttack monsterAttack;

    private void Update()
    {
        if (gameObject.activeSelf && CurrentHealth > 0)
        {
            lifeLine.fillAmount = (float)CurrentHealth / Health;

            textHP.text = ShortCutValue.shortCutValue.CutIntValue(CurrentHealth, 2);

            rewardGot = false;
        }
        else
        {
            if (!rewardGot)
            {
                rewardGot = true;
                monsterAttack.IsAttack = false;
                monsterAttack.RedEffect.SetActive(false);
                tapOnMonster.MonsterHasHP = false;
                CurrentHealth = 0;
                lifeLine.fillAmount = (float)CurrentHealth / Health;
                textHP.text = ShortCutValue.shortCutValue.CutIntValue(CurrentHealth, 1);

                fightAnimation.MonsterIsDefeat();
            }
        }
    }
}
