using UnityEngine;
using UnityEngine.UI;
using YG;

public class GunSceneController : MonoBehaviour
{
    // Panels

    [SerializeField] private GameObject panelAllGuns, panelGunInfo;

    // UI Objects

    [SerializeField] private GameObject[] framesSelected;
    [SerializeField] private GameObject[] textLocked;
    [SerializeField] private GameObject[] gunsButtons;
    [SerializeField] private Image[] gunsImages;

    [SerializeField] private Image gunInfoImage;
    [SerializeField] private Sprite[] gunsImagesInfoPanel;
    [SerializeField] private GameObject[] personalBlocks;
    [SerializeField] private Text critText, damageText;

    // Technical Objects

    private int indexOfOpenWeapon;
    private int[] costOfWeapons = new int[10] { 0, 100, 500, 1200, 2500, 4500, 7500, 10000, 15000, 20000 };
    private int[] damageOfWeapons = new int[10] { 1, 2, 2, 3, 3, 4, 5, 5, 7, 7 };
    private int[] critOfWeapons = new int[10] { 1, 1, 18, 2, 20, 10, 5, 15, 5, 25 };
    [SerializeField] private GameObject buttonBuy, buttonSelect;
    [SerializeField] private Text costText;
    [SerializeField] private GameObject leftArrow, rightArrow;

    [SerializeField] private WeaponSceneLearning weaponSceneLearning;

    private void Start()
    {
        UpdateAllGunsPanel();
    }

    public void OpenGunInfoPanel(int gunIndex)
    {
        UpdateInfoPanel(gunIndex);
        indexOfOpenWeapon = gunIndex;

        panelGunInfo.SetActive(true);
        panelAllGuns.SetActive(false);

        if (YandexGame.savesData.CurrentLearningStep == 10)
        {
            weaponSceneLearning.SetStepSettings();
        }
    }

    public void UpdateInfoPanel(int gunIndex)
    {
        for (int i = 0; i < personalBlocks.Length; i++)
        {
            if (gunIndex != i)
            {
                personalBlocks[i].SetActive(false);
            }
            else
            {
                personalBlocks[i].SetActive(true);
                gunInfoImage.sprite = gunsImagesInfoPanel[i];
                if (YandexGame.savesData.WeaponPurchased[gunIndex]) { gunInfoImage.color = Color.white; }
                else { gunInfoImage.color = Color.black; }
                critText.text = critOfWeapons[i].ToString() + "%";
                damageText.text = damageOfWeapons[i].ToString();
                costText.text = costOfWeapons[i].ToString();


                buttonSelect.SetActive(!(YandexGame.savesData.WeaponSelected == i) && YandexGame.savesData.WeaponPurchased[i]);
                buttonBuy.SetActive(!YandexGame.savesData.WeaponPurchased[i]);
            }

            // Learning
            if (YandexGame.savesData.CurrentLearningStep > 15)
            {
                leftArrow.SetActive(gunIndex != 0);
                rightArrow.SetActive(gunIndex != gunsImagesInfoPanel.Length - 1 && !textLocked[gunIndex + 1].activeSelf);
            }
        }

        if (YandexGame.savesData.Coins < costOfWeapons[gunIndex])
        {
            buttonBuy.GetComponent<Button>().enabled = false;
            buttonBuy.GetComponent<Image>().color = new Color32(100, 100, 100, 255);
        }
        else
        {
            buttonBuy.GetComponent<Button>().enabled = true;
            buttonBuy.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
    }

    public void UpdateAllGunsPanel()
    {
        for (int i = 0; i < gunsImages.Length; i++)
        {
            framesSelected[i].SetActive(YandexGame.savesData.WeaponSelected == i);

            if (YandexGame.savesData.WeaponPurchased[i])
            {
                gunsImages[i].color = Color.white;
            }
            else
            {
                gunsImages[i].color = Color.black;
            }
        }

        if (YandexGame.savesData.Level < 10) { LockWeapon(2); }
        if (YandexGame.savesData.Level < 20) { LockWeapon(3); }
        if (YandexGame.savesData.Level < 25) { LockWeapon(4); }
        if (YandexGame.savesData.Level < 35) { LockWeapon(5); }
        if (YandexGame.savesData.Level < 40) { LockWeapon(6); }
        if (YandexGame.savesData.Level < 50) { LockWeapon(7); }
        if (YandexGame.savesData.Level < 55) { LockWeapon(8); }
        if (YandexGame.savesData.Level < 70) { LockWeapon(9); }
    }

    private void LockWeapon(int index)
    {
        gunsButtons[index].GetComponent<Button>().enabled = false;
        gunsButtons[index].GetComponent<Image>().color = new Color32(100, 100, 100, 150);
        textLocked[index].SetActive(true);
    }

    public void CloseGunInfoPanel()
    {
        panelGunInfo.SetActive(false);
        panelAllGuns.SetActive(true);

        if (YandexGame.savesData.CurrentLearningStep == 14)
        {
            weaponSceneLearning.SetStepSettings();
        }
    }

    public void SelectWeapon()
    {
        YandexGame.savesData.WeaponSelected = indexOfOpenWeapon;
        YandexGame.savesData.WeaponDamage = damageOfWeapons[indexOfOpenWeapon];
        YandexGame.savesData.WeaponCrit = critOfWeapons[indexOfOpenWeapon];
        YandexGame.SaveProgress();
        UpdateInfoPanel(indexOfOpenWeapon);
        UpdateAllGunsPanel();

        if (YandexGame.savesData.CurrentLearningStep==11)
        {
            weaponSceneLearning.SetStepSettings();
        }
    }

    public void BuyWeapon()
    {
        YandexGame.savesData.Coins -= costOfWeapons[indexOfOpenWeapon];
        YandexGame.savesData.WeaponPurchased[indexOfOpenWeapon] = true;
        SelectWeapon();
    }

    public void ArrowClick(int delta)
    {
        UpdateInfoPanel(indexOfOpenWeapon + delta);
        indexOfOpenWeapon = indexOfOpenWeapon + delta;
    }
}
