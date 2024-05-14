using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class CampainController : MonoBehaviour
{
    // UI Objects

    private MonsterHP monsterHP;
    [SerializeField] private GameObject[] monsters = new GameObject[9];
    [SerializeField] private GameObject[] namesMonsters = new GameObject[9];
    [SerializeField] private GameObject healthContainer, attackContainer;
    [SerializeField] private GameObject buttonSeek, buttonGoSampleScene, weaponContainer;
    [SerializeField] private GameObject tapPanel;
    [SerializeField] private Image weaponImage;
    [SerializeField] private Text damageText, critText, levelValueText, stepText, attackText;
    [SerializeField] private TapOnMonster tapOnMonster;
    [SerializeField] private FightAnimation fightAnimation;
    [SerializeField] private BossAnimation bossAnimation;

    // Constants

    public int CurrentStep = 0;
    private int currentLevel = 0;

    [SerializeField] private int[] countMonstersOnLevel = new int[10];
    [SerializeField] private IntStepValue[] typeMonstersOnLevel = new IntStepValue[10];
    [SerializeField] private IntStepValue[] monstersHP = new IntStepValue[10];
    [SerializeField] private IntStepValue[] monstersAttack = new IntStepValue[10];
    [SerializeField] private IntStepValue[] attackCooldown = new IntStepValue[10];
    [SerializeField] private IntStepValue[] rewardsMonstersOnLevel = new IntStepValue[10];
    [SerializeField] private IntStepValue[] countRewardsOnLevel = new IntStepValue[10];
    [SerializeField] private IntStepValue[] chansesPet = new IntStepValue[10];
    [SerializeField] private Sprite[] weaponImages = new Sprite[10];
    [SerializeField] private GameObject[] hitEffect = new GameObject[10];
    [SerializeField] private GameObject[] critEffect = new GameObject[10];
    [SerializeField] private MonsterAttack monsterAttack;

    [SerializeField] PlayClip dangerSound;

    private void Awake()
    {
        currentLevel = PlayerStats.playerStats.LevelIndex;
        levelValueText.text = (currentLevel + 1).ToString();
        UpdateStepUI();
    }

    public void SpawnNextStep()
    {
        if (CurrentStep != countMonstersOnLevel[currentLevel] - 1)
        {
            SpawnSimpleStep();
        }
        else
        {
            SpawnSimpleStep();

            bossAnimation.MonsterContainer = monsters[typeMonstersOnLevel[currentLevel].Step[CurrentStep]];
            bossAnimation.StartAnimation();

            dangerSound.PlayOneShot();

            namesMonsters[typeMonstersOnLevel[currentLevel].Step[CurrentStep]].SetActive(false);

            tapPanel.SetActive(false);
            healthContainer.SetActive(false);
            attackContainer.SetActive(false);
            weaponContainer.SetActive(false);

            Invoke("ActivateBossFunctions", 4);
        }
    }

    public void ActivateBossFunctions()
    {
        tapPanel.SetActive(true);
        healthContainer.SetActive(true);
        attackContainer.SetActive(true);
        weaponContainer.SetActive(true);
        namesMonsters[typeMonstersOnLevel[currentLevel].Step[CurrentStep]].SetActive(true);
    }

    public void SpawnSimpleStep()
    {
        fightAnimation.MonsterContainer = monsters[typeMonstersOnLevel[currentLevel].Step[CurrentStep]];
        fightAnimation.MonsterName = namesMonsters[typeMonstersOnLevel[currentLevel].Step[CurrentStep]];
        fightAnimation.TypeOfReward = rewardsMonstersOnLevel[currentLevel].Step[CurrentStep];
        fightAnimation.CountOfReward = countRewardsOnLevel[currentLevel].Step[CurrentStep];
        fightAnimation.ChansesPets = chansesPet[currentLevel].Step;

        monsterHP = monsters[typeMonstersOnLevel[currentLevel].Step[CurrentStep]].GetComponent<MonsterHP>();
        tapOnMonster.Monster = monsters[typeMonstersOnLevel[currentLevel].Step[CurrentStep]].GetComponent<MonsterHP>(); // Replace on MonsterHP
        monsterHP.Health = monstersHP[currentLevel].Step[CurrentStep];
        monsterHP.CurrentHealth = monstersHP[currentLevel].Step[CurrentStep];

        monsters[typeMonstersOnLevel[currentLevel].Step[CurrentStep]].SetActive(true);
        namesMonsters[typeMonstersOnLevel[currentLevel].Step[CurrentStep]].SetActive(true);

        SetOverallConditions();
    }

    public void SetOverallConditions()
    {
        buttonSeek.SetActive(false);
        buttonGoSampleScene.SetActive(false);

        if (YandexGame.savesData.CurrentLearningStep != 50) { tapPanel.SetActive(true); }
        healthContainer.SetActive(true);
        attackContainer.SetActive(true);
        weaponContainer.SetActive(true);

        weaponImage.sprite = weaponImages[YandexGame.savesData.WeaponSelected];
        damageText.text = ShortCutValue.shortCutValue.CutIntValue(YandexGame.savesData.WeaponDamage * PlayerStats.playerStats.AttackPlayer, 2);
        critText.text = YandexGame.savesData.WeaponCrit.ToString() + "%";
        attackText.text = ShortCutValue.shortCutValue.CutIntValue(monstersAttack[currentLevel].Step[CurrentStep], 2);


        // Change on Monster
        tapOnMonster.MonsterHasHP = true;
        tapOnMonster.criticalChance = YandexGame.savesData.WeaponCrit;
        tapOnMonster.hitValue = YandexGame.savesData.WeaponDamage * PlayerStats.playerStats.AttackPlayer;

        tapOnMonster.HitEffect = hitEffect[YandexGame.savesData.WeaponSelected];
        tapOnMonster.CritEffect = critEffect[YandexGame.savesData.WeaponSelected];

        monsterAttack.AttackCooldown = attackCooldown[currentLevel].Step[CurrentStep];
        monsterAttack.DamageValue = monstersAttack[currentLevel].Step[CurrentStep];
        monsterAttack.StartAttack();
    }

    public void UpdateCompletedlevels()
    {
        YandexGame.savesData.CompletedLevels[YandexGame.savesData.OpenIslandOnSampleScene, currentLevel] = true;
        if (currentLevel!=9) { YandexGame.savesData.OpenLevels[YandexGame.savesData.OpenIslandOnSampleScene, currentLevel + 1] = true; }
        YandexGame.SaveProgress();
    }

    public void UpdateStepUI()
    {
        stepText.text = (CurrentStep + 1).ToString() + " / " + countMonstersOnLevel[currentLevel].ToString();
    }
}

[System.Serializable] public class IntStepValue
{
    public int[] Step;
}
