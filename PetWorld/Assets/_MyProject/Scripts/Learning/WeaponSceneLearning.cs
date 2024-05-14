using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using YG;

public class WeaponSceneLearning : MonoBehaviour, IPointerClickHandler
{
    [Header("_________________ Objects on scene _________________")]
    [Space(20)]

    [SerializeField] private GameObject[] objectsHideInLearning;
    [SerializeField] private GameObject buttonBackWorld, buttonBack, attack, crit, buttonWeapon;

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

            if (YandexGame.savesData.CurrentLearningStep == 9)
            {
                buttonBackWorld.SetActive(false);
                buttonBack.SetActive(false);
                attack.SetActive(false);
                crit.SetActive(false);
                buttonWeapon.SetActive(false);

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
        textsOfPet[YandexGame.savesData.CurrentLearningStep - 9].SetActive(true);
    }

    public void SpawnNextStep()
    {
        textsOfPet[YandexGame.savesData.CurrentLearningStep - 9].SetActive(true);

        switch(YandexGame.savesData.CurrentLearningStep)
        {
            case 10:
                buttonWeapon.SetActive(true);
                tapIsLocked = true;
                break;
            case 11:
                buttonWeapon.GetComponent<Button>().enabled = false;                
                break;
            case 12:
                learningPet1.GetComponent<Animator>().enabled = false ;
                learningPet1.SetActive(false);
                learningPet2.SetActive(true);
                attack.SetActive(true);
                tapIsLocked = false;
                break;
            case 13:
                crit.SetActive(true);
                break;
            case 14:
                tapIsLocked = true;
                buttonBack.SetActive(true);
                break;
            case 15:
                lowBorder.SetActive(false);
                learningPet2.SetActive(false);
                learningPet1.SetActive(true);
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
        textsOfPet[YandexGame.savesData.CurrentLearningStep - 9].SetActive(false);
        YandexGame.savesData.CurrentLearningStep++;
        YandexGame.SaveProgress();

        timeForRead = 0;
        SpawnNextStep();
    }
}
