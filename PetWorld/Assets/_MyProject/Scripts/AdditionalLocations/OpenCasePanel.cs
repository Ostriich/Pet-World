using UnityEngine;
using UnityEngine.EventSystems;

public class OpenCasePanel : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject[] rewards = new GameObject[6];
    [SerializeField] private GameObject buttonBack;
    [SerializeField] private GameObject textTap;

    [SerializeField] private PlayClip getReward, getCommonPet, getRarePet, getMythicPet, getLegendaryPet;

    private int currentStep;

    public int IndexOfCase;

    public void SetDefaultSettings()
    {
        foreach (GameObject rewardCase in rewards)
        {
            rewardCase.GetComponent<RectTransform>().localPosition = new Vector3(-600, 0, 0);
            rewardCase.GetComponent<RectTransform>().localScale = new Vector3(0.1f, 0.1f, 0.1f);
            rewardCase.GetComponent<Animator>().SetBool("Start", false);
        }

        buttonBack.SetActive(false);
        textTap.SetActive(true);
        currentStep = 0;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (textTap.activeSelf)
        {
            rewards[currentStep].GetComponent<Animator>().SetBool("Start", true);

            if (currentStep == 0 || currentStep == 1) { getReward.PlayOneShot(); } 

            switch (IndexOfCase)
            {
                case 0:
                    if (currentStep != 0 && currentStep != 1) { getCommonPet.PlayOneShot(); }
                    if (currentStep == 3) { EndOpenCase(); }
                    break;
                case 1:
                    if (currentStep == 4) 
                    {
                        getRarePet.PlayOneShot();
                        EndOpenCase(); 
                    }
                    else if (currentStep != 0 && currentStep != 1)
                    {
                        getCommonPet.PlayOneShot();
                    }
                    break;
                case 2:
                    if (currentStep == 5) 
                    {
                        getMythicPet.PlayOneShot();
                        EndOpenCase(); 
                    }
                    else if (currentStep == 2)
                    {
                        getCommonPet.PlayOneShot();
                    }
                    else if(currentStep != 0 && currentStep != 1)
                    {
                        getRarePet.PlayOneShot();
                    }
                    break;
                case 3:
                    if (currentStep == 5)
                    {
                        getLegendaryPet.PlayOneShot();
                        EndOpenCase();
                    }
                    else if (currentStep == 2)
                    {
                        getRarePet.PlayOneShot();
                    }
                    else if (currentStep != 0 && currentStep != 1)
                    {
                        getMythicPet.PlayOneShot();
                    }
                    break;
            }
        }

        currentStep++;
    }

    private void EndOpenCase()
    {
        textTap.SetActive(false);
        PlayerStats.playerStats.UpdateStats();
        Invoke("OpenButtonBack", 2);
    }

    private void OpenButtonBack()
    {
        buttonBack.SetActive(true);
    }
}
