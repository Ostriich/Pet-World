using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using YG;

public class TapOnEgg : MonoBehaviour, IPointerClickHandler
{
    public int hitValue, criticalChance;
    public GameObject HitEffect, CritEffect, hitValueText, critValueText;

    public bool eggHasHP;
    public EggHP Egg;
    public EggTapAnimation eggTapAnimation;

    // Extra Shoots

    [SerializeField] private GameObject buttonExtraShoots;
    [SerializeField] private Text countOfExtraShoots;
    [SerializeField] private GameObject buyExtraShootsPanel;
    [SerializeField] private PlayClip playerHitSound, playerCritSound;

    [SerializeField] private GameObject plusButton;

    private float timerOfExtraShoots = 5;
    private bool extraShootsActive = false;

    [SerializeField] private World1Learning world1Learning;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (YandexGame.savesData.CurrentLearningStep == 29) { world1Learning.SetStepSettings(); }

        if (eggHasHP && YandexGame.savesData.CurrentLearningStep != 31)
        {
            eggTapAnimation.Multiplier = -1;

            // Count probability of Critical damage
            int randomChance = Random.Range(0, 100);

            if (randomChance < criticalChance) 
            {
                Egg.CurrentHealth -= hitValue * 3;
                SpawnEffect(CritEffect, critValueText, hitValue * 3);
                YandexGame.savesData.Exp += hitValue * 3;

                playerCritSound.PlayOneShot();
            }
            else
            {
                Egg.CurrentHealth -= hitValue;
                SpawnEffect(HitEffect, hitValueText, hitValue);
                YandexGame.savesData.Exp += hitValue;

                playerHitSound.PlayOneShot();
            }
            PlayerStats.playerStats.UpdateStats();
        }
    }

    private void SpawnEffect(GameObject effect, GameObject valueText, int valueHit)
    {
        // Determine the point of spawn
        int angle = Random.Range(0, 359);
        float radius = Random.Range(10, 40);
        radius /= 10;
        float x = Mathf.Cos(angle) * radius;
        float y = Mathf.Sin(angle) * radius;

        // Spawn the clickSpirit
        Instantiate(effect, new Vector3(x, y, 0), Quaternion.identity);

        GameObject clickText = Instantiate(valueText, new Vector3(x, y, 0), Quaternion.identity);
        clickText.GetComponent<TextMeshPro>().text = ShortCutValue.shortCutValue.CutIntValue(valueHit, 1);
    }

    public void StartExtraShoots()
    {
        if (YandexGame.savesData.CurrentLearningStep == 31) { world1Learning.SetStepSettings(); }

        if (YandexGame.savesData.ExtraShoots > 0)
        {
            extraShootsActive = true;
            YandexGame.savesData.ExtraShoots--;
            YandexGame.SaveProgress();
            ExtraShoot();
        }
        else
        {
            buyExtraShootsPanel.SetActive(true);
        }
    }

    private void ExtraShoot()
    {
        if (extraShootsActive)
        {
            eggTapAnimation.Multiplier = -1;

            Egg.CurrentHealth -= hitValue * 3;
            SpawnEffect(CritEffect, critValueText, hitValue * 3);
            YandexGame.savesData.Exp += hitValue * 3;

            PlayerStats.playerStats.UpdateStats();

            playerCritSound.PlayOneShot();

            Invoke("ExtraShoot", 0.1f);
        }
    }

    private void Update()
    {
        countOfExtraShoots.text = YandexGame.savesData.ExtraShoots.ToString();

        if (YandexGame.savesData.ExtraShoots == 0)
        {
            buttonExtraShoots.GetComponent<Image>().color = new Color32(255, 255, 255, 150);
            plusButton.SetActive(true);
        }
        else
        {
            buttonExtraShoots.GetComponent<Image>().color = Color.white;
            plusButton.SetActive(false);
        }

        if (extraShootsActive)
        {
            timerOfExtraShoots -= Time.deltaTime;

            buttonExtraShoots.GetComponent<Button>().enabled = false;
            buttonExtraShoots.GetComponent<Image>().fillAmount = timerOfExtraShoots / 5;

            if (timerOfExtraShoots <= 0 || !eggHasHP)
            {
                extraShootsActive = false;
                timerOfExtraShoots = 5;
            }
        }
        else
        {
            buttonExtraShoots.GetComponent<Button>().enabled = true;
            buttonExtraShoots.GetComponent<Image>().fillAmount = 1;
        }
    }
}
