using UnityEngine;
using UnityEngine.EventSystems;
using YG;

public class MonstersLearning : MonoBehaviour, IPointerClickHandler
{
    [Header("_________________ Objects on scene _________________")]
    [Space(20)]

    [SerializeField] private GameObject[] objectsHideInLearning;
    [SerializeField] private GameObject buttonSearch, tapPanel;

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

            if (YandexGame.savesData.CurrentLearningStep == 47)
            {
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
        learningPet2.SetActive(true);
        textsOfPet[YandexGame.savesData.CurrentLearningStep - 47].SetActive(true);
    }

    public void SpawnNextStep()
    {
        textsOfPet[YandexGame.savesData.CurrentLearningStep - 47].SetActive(true);

        switch (YandexGame.savesData.CurrentLearningStep)
        {
            case 48:
                tapIsLocked = true;
                lowBorder.SetActive(false);
                learningPet2.SetActive(false);
                buttonSearch.SetActive(true);
                break;
            case 50:
                tapIsLocked = false;
                lowBorder.SetActive(true);
                learningPet2.SetActive(true);
                break;
            case 53:
                tapIsLocked = true;
                lowBorder.SetActive(false);
                learningPet2.SetActive(false);
                tapPanel.SetActive(true);
                break;
            case 54:
                tapIsLocked = false;
                lowBorder.SetActive(true);
                learningPet2.SetActive(true);
                break;
            case 56:
                tapIsLocked = true;
                lowBorder.SetActive(false);
                learningPet2.SetActive(false);
                buttonSearch.SetActive(true);
                break;
            case 57:
                tapIsLocked = false;
                lowBorder.SetActive(true);
                learningPet2.SetActive(true);
                break;
            case 59:
                tapIsLocked = true;
                lowBorder.SetActive(false);
                learningPet2.SetActive(false);
                buttonSearch.SetActive(true);
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
        textsOfPet[YandexGame.savesData.CurrentLearningStep - 47].SetActive(false);
        YandexGame.savesData.CurrentLearningStep++;
        YandexGame.SaveProgress();

        timeForRead = 0;
        SpawnNextStep();
    }
}
