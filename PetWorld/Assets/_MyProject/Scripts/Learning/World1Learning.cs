using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using YG;

public class World1Learning : MonoBehaviour, IPointerClickHandler
{
    [Header("_________________ Objects on scene _________________")]
    [Space(20)]

    [SerializeField] private GameObject[] objectsHideInLearning;
    [SerializeField] private GameObject buttonBackWorld, buttonSearch, buttonGoPets, buttonExtraShoot;

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

            if (YandexGame.savesData.CurrentLearningStep == 17)
            {
                buttonBackWorld.SetActive(false);
                buttonGoPets.SetActive(false);
                buttonExtraShoot.SetActive(false);
                buttonSearch.SetActive(false);

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
        textsOfPet[YandexGame.savesData.CurrentLearningStep - 17].SetActive(true);
    }

    public void SpawnNextStep()
    {
        textsOfPet[YandexGame.savesData.CurrentLearningStep - 17].SetActive(true);

        switch (YandexGame.savesData.CurrentLearningStep)
        {
            case 27:
                tapIsLocked = true;
                buttonSearch.SetActive(true);
                break;
            case 28:
                buttonSearch.SetActive(false);
                lowBorder.SetActive(false);
                learningPet1.SetActive(false);
                break;
            case 29:
                lowBorder.SetActive(true);
                learningPet2.SetActive(true);
                break;
            case 30:
                lowBorder.SetActive(false);
                learningPet2.SetActive(false);
                break;
            case 31:
                lowBorder.SetActive(true);
                learningPet2.SetActive(true);
                buttonExtraShoot.SetActive(true);
                break;
            case 32:
                lowBorder.SetActive(false);
                learningPet2.SetActive(false);
                break;
            case 33:
                lowBorder.SetActive(true);
                learningPet2.SetActive(true);
                tapIsLocked = false;
                break;
            case 37:
                tapIsLocked = true;
                buttonGoPets.SetActive(true);
                lowBorder.SetActive(false);
                learningPet2.SetActive(false);
                gameObject.SetActive(false);
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
        textsOfPet[YandexGame.savesData.CurrentLearningStep - 17].SetActive(false);
        YandexGame.savesData.CurrentLearningStep++;
        YandexGame.SaveProgress();

        timeForRead = 0;
        SpawnNextStep();
    }
}
