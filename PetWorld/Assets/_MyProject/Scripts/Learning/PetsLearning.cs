using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using YG;

public class PetsLearning : MonoBehaviour, IPointerClickHandler
{
    [Header("_________________ Objects on scene _________________")]
    [Space(20)]

    [SerializeField] private GameObject[] objectsHideInLearning;
    [SerializeField] private GameObject buttonBackWorld, buttonBack, attack, health, buttonSell, buttonFeed, foodStat;
    [SerializeField] private Button[] lockedButtons = new Button[9];
    [SerializeField] private Button butPet;

    [Header("_________________ Learning objects _________________")]
    [Space(20)]

    [SerializeField] private GameObject lowBorder;
    [SerializeField] private GameObject learningPet1, learningPet2;
    [SerializeField] private GameObject[] textsOfPet;
    [SerializeField] private GameObject tapToContinue;

    [SerializeField] private PlayClip playClip;

    private float timeForRead;
    private bool tapIsLocked;

    private void Start()
    {
        if (YandexGame.savesData.CurrentLearningStep >= 63) { gameObject.SetActive(false); }
        else
        {
            foreach (GameObject obj in objectsHideInLearning) { obj.SetActive(false); }
            foreach (Button but in lockedButtons) { but.enabled = false; }

            if (YandexGame.savesData.CurrentLearningStep == 37)
            {
                buttonBackWorld.SetActive(false);
                buttonBack.SetActive(false);
                attack.SetActive(false);
                health.SetActive(false);
                buttonSell.SetActive(false);
                buttonFeed.SetActive(false);
                foodStat.SetActive(false);

                SpawnFirstStep();
            }
        }
    }

    private void FixedUpdate()
    {
        timeForRead += Time.deltaTime;
    }

    private void Update()
    {
        if (timeForRead >= 5 && !tapIsLocked) { tapToContinue.SetActive(true); }
        else { tapToContinue.SetActive(false); }
    }

    private void SpawnFirstStep()
    {
        timeForRead = 0;
        lowBorder.SetActive(true);
        learningPet1.SetActive(true);
        textsOfPet[YandexGame.savesData.CurrentLearningStep - 37].SetActive(true);
        tapIsLocked = true;
        GetComponent<Image>().enabled = false;
    }

    public void SpawnNextStep()
    {
        textsOfPet[YandexGame.savesData.CurrentLearningStep - 37].SetActive(true);

        switch (YandexGame.savesData.CurrentLearningStep)
        {
            case 38:
                learningPet1.SetActive(false);
                learningPet2.SetActive(true);
                butPet.enabled = false;
                tapIsLocked = false;
                GetComponent<Image>().enabled = true;
                break;
            case 39:
                health.SetActive(true);
                attack.SetActive(true);
                break;
            case 40:
                buttonSell.SetActive(true);
                break;
            case 41:
                foodStat.SetActive(true);
                buttonFeed.SetActive(true);
                break;
            case 42:
                lowBorder.SetActive(false);
                learningPet2.SetActive(false);
                tapIsLocked = true;
                GetComponent<Image>().enabled = false;
                break;
            case 43:
                lowBorder.SetActive(true);
                learningPet2.SetActive(true);
                tapIsLocked = false;
                GetComponent<Image>().enabled = true;
                break;
            case 44:
                lowBorder.SetActive(false);
                learningPet2.SetActive(false);
                buttonBack.SetActive(true);
                tapIsLocked = true;
                GetComponent<Image>().enabled = false;
                break;
            case 45:
                buttonBackWorld.SetActive(true);
                break;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (timeForRead >= 0.1 && !tapIsLocked)
        {
            SetStepSettings();
            playClip.PlayOneShot();
        }
    }

    public void SetStepSettings()
    {
        textsOfPet[YandexGame.savesData.CurrentLearningStep - 37].SetActive(false);
        YandexGame.savesData.CurrentLearningStep++;
        YandexGame.SaveProgress();

        timeForRead = 0;
        SpawnNextStep();
    }
}
