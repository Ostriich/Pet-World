using UnityEngine;
using UnityEngine.UI;
using YG;

public class EggController : MonoBehaviour
{
    // UI Objects

    [SerializeField] private GameObject commonEgg, rareEgg;
    private EggHP eggHP;
    [SerializeField] private GameObject healthContainer;
    [SerializeField] private GameObject buttonSeek, buttonGoSampleScene, buttonShoot, weaponContainer, probabilityDropContainer;
    [SerializeField] private GameObject tapPanel;
    [SerializeField] private Image weaponImage;
    [SerializeField] private Text damageText, critText;
    [SerializeField] private Text[] probabilityDropItems = new Text[4];
    [SerializeField] private TapOnEgg tapOnEgg;
    [SerializeField] private CrackAnimation crackAnimation;

    // Constants

    [SerializeField] private int commonEggHP, rareEggHP;
    [SerializeField] private Sprite[] weaponImages = new Sprite[10];
    [SerializeField] private GameObject[] hitEffect = new GameObject[10];
    [SerializeField] private GameObject[] critEffect = new GameObject[10];

    public void SpawnCommonEgg()
    {
        crackAnimation.EggContainer = commonEgg;
        eggHP = commonEgg.GetComponent<EggHP>();
        tapOnEgg.eggTapAnimation = commonEgg.GetComponent<EggTapAnimation>();
        eggHP.Health = commonEggHP;
        eggHP.CurrentHealth = commonEggHP;
        tapOnEgg.Egg = commonEgg.GetComponent<EggHP>();

        rareEgg.SetActive(false);
    
        commonEgg.SetActive(true);
        
        probabilityDropItems[0].text = "80%";
        probabilityDropItems[1].text = "15%";
        probabilityDropItems[2].text = "5%";
        probabilityDropItems[3].text = "0%";

        SetOverallConditions();
    }

    public void SpawnRareEgg()
    {
        crackAnimation.EggContainer = rareEgg;
        eggHP = rareEgg.GetComponent<EggHP>();
        tapOnEgg.eggTapAnimation = rareEgg.GetComponent<EggTapAnimation>();
        eggHP.Health = rareEggHP;
        eggHP.CurrentHealth = rareEggHP;
        tapOnEgg.Egg = rareEgg.GetComponent<EggHP>();

        commonEgg.SetActive(false);

        rareEgg.SetActive(true);

        probabilityDropItems[0].text = "0%";
        probabilityDropItems[1].text = "80%";
        probabilityDropItems[2].text = "15%";
        probabilityDropItems[3].text = "5%";

        SetOverallConditions();
    }

    public void SetOverallConditions()
    {
        buttonSeek.SetActive(false);
        buttonGoSampleScene.SetActive(false);

        tapPanel.SetActive(true);
        healthContainer.SetActive(true);
        if (YandexGame.savesData.CurrentLearningStep >= 63) { buttonShoot.SetActive(true); }
        weaponContainer.SetActive(true);
        if (YandexGame.savesData.CurrentLearningStep >= 63) { probabilityDropContainer.SetActive(true); }

        weaponImage.sprite = weaponImages[YandexGame.savesData.WeaponSelected];
        damageText.text = YandexGame.savesData.WeaponDamage.ToString();
        critText.text = YandexGame.savesData.WeaponCrit.ToString() + "%";

        tapOnEgg.eggHasHP = true;
        tapOnEgg.criticalChance = YandexGame.savesData.WeaponCrit;
        tapOnEgg.hitValue = YandexGame.savesData.WeaponDamage;

        tapOnEgg.HitEffect = hitEffect[YandexGame.savesData.WeaponSelected];
        tapOnEgg.CritEffect = critEffect[YandexGame.savesData.WeaponSelected];
    }
}
