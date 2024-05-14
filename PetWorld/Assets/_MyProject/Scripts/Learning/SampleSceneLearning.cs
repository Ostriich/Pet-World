using UnityEngine;
using UnityEngine.EventSystems;
using YG;

public class SampleSceneLearning : MonoBehaviour, IPointerClickHandler
{
    [Header("_________________ Objects on scene _________________")]
    [Space(20)]

    [SerializeField] private GameObject[] objectsHideInLearning;
    [SerializeField] private GameObject buttonWorld, buttonCampain, buttonWeapons, buttonPets, UIStats, backOutCampainButton;

    [Header("_________________ Learning objects _________________")]
    [Space(20)]

    [SerializeField] private GameObject lowBorder;
    [SerializeField] private GameObject learningPet, learningPet2;
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

            if (YandexGame.savesData.CurrentLearningStep > 0 && YandexGame.savesData.CurrentLearningStep < 15) { YandexGame.savesData.CurrentLearningStep = 0; }
            if (YandexGame.savesData.CurrentLearningStep >= 15 && YandexGame.savesData.CurrentLearningStep < 45) { YandexGame.savesData.CurrentLearningStep = 15; }
            if (YandexGame.savesData.CurrentLearningStep >= 45 && YandexGame.savesData.CurrentLearningStep < 60) { YandexGame.savesData.CurrentLearningStep = 45; }

            switch (YandexGame.savesData.CurrentLearningStep)
            {
                case 0:
                    buttonCampain.SetActive(false);
                    buttonWorld.SetActive(false);
                    buttonWeapons.SetActive(false);
                    buttonPets.SetActive(false);
                    UIStats.SetActive(false);

                    Invoke("SpawnFirstStep", 2);
                    break;
                case 15:
                    timeForRead = 0;
                    lowBorder.SetActive(true);
                    learningPet.SetActive(true);

                    buttonCampain.SetActive(false);
                    buttonWorld.SetActive(false);
                    buttonWeapons.SetActive(false);
                    buttonPets.SetActive(false);
                    UIStats.SetActive(false);

                    SpawnNextStep();
                    break;
                case 45:
                    timeForRead = 0;
                    lowBorder.SetActive(true);
                    learningPet2.SetActive(true);

                    buttonCampain.SetActive(false);
                    buttonWorld.SetActive(false);
                    buttonWeapons.SetActive(false);
                    buttonPets.SetActive(false);

                    SpawnNextStep();
                    break;
                case 60:
                    timeForRead = 0;
                    lowBorder.SetActive(true);
                    learningPet.SetActive(true);

                    buttonCampain.SetActive(false);
                    buttonWorld.SetActive(false);
                    buttonWeapons.SetActive(false);
                    buttonPets.SetActive(false);
                    SpawnNextStep();
                    break;
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
        learningPet.SetActive(true);
        textsOfPet[YandexGame.savesData.CurrentLearningStep].SetActive(true);
    }

    private void SpawnNextStep()
    {
        textsOfPet[YandexGame.savesData.CurrentLearningStep].SetActive(true);

        switch (YandexGame.savesData.CurrentLearningStep)
        {
            case 9:
                lowBorder.SetActive(false);
                tapIsLocked = true;
                buttonWeapons.SetActive(true);
                break;
            case 17:
                lowBorder.SetActive(false);
                tapIsLocked = true;
                buttonWorld.SetActive(true);
                break;
            case 46:
                lowBorder.SetActive(false);
                learningPet2.SetActive(false);
                tapIsLocked = true;
                buttonCampain.SetActive(true);
                break;
            case 47:
                backOutCampainButton.SetActive(false);
                break;
            case 63:
                tapIsLocked = true;
                timeForRead = 0;
                lowBorder.SetActive(false);
                learningPet.SetActive(false);

                foreach (GameObject obj in objectsHideInLearning) { obj.SetActive(true); }
                buttonCampain.SetActive(true);
                buttonWorld.SetActive(true);
                buttonWeapons.SetActive(true);
                buttonPets.SetActive(true);

                gameObject.SetActive(false);
                break;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (timeForRead >= 0.1 && !tapIsLocked && YandexGame.savesData.CurrentLearningStep>=1)
        {
            playClip.PlayOneShot();
            SetNewStep();
        }
        else if (timeForRead >= 2 && YandexGame.savesData.CurrentLearningStep == 0)
        {
            playClip.PlayOneShot();
            SetNewStep();
        }
    }

    public void SetNewStep()
    {
        textsOfPet[YandexGame.savesData.CurrentLearningStep].SetActive(false);
        YandexGame.savesData.CurrentLearningStep++;
        YandexGame.SaveProgress();

        timeForRead = 0;
        SpawnNextStep();
    }
}
