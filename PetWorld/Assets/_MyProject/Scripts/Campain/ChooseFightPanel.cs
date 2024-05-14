using UnityEngine;
using UnityEngine.UI;
using YG;
using UnityEngine.SceneManagement;

public class ChooseFightPanel : MonoBehaviour
{
    // process objects

    public int selectedWorld = 0;
    [SerializeField] private string[] worldFightScenes = new string[5];

    // UI objects

    [SerializeField] private GameObject bgWorldPanel;
    [SerializeField] private Sprite[] worldBGs = new Sprite[5];
    [SerializeField] private GameObject[] buttonLevels = new GameObject[10];
    [SerializeField] private Text[] textLevels = new Text[10];
    [SerializeField] private Sprite greenButton, whiteButton;

    [SerializeField] private SampleSceneLearning sampleSceneLearning;

    /*
    private void Start()
    {
        UpdateWorldFightInfo();
    }
    */

    public void UpdateWorldFightInfo()
    {
        bgWorldPanel.GetComponent<Image>().sprite = worldBGs[selectedWorld];

        for (int i = 0; i < buttonLevels.Length; i++)
        {
            if (YandexGame.savesData.CompletedLevels[selectedWorld,i])
            {
                buttonLevels[i].GetComponent<Button>().enabled = true;
                buttonLevels[i].GetComponent<Image>().sprite = greenButton;
                buttonLevels[i].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                textLevels[i].color = new Color32(0, 100, 0, 255);
            }
            else
            {
                buttonLevels[i].GetComponent<Button>().enabled = true;
                buttonLevels[i].GetComponent<Image>().sprite = whiteButton;
                buttonLevels[i].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                textLevels[i].color = new Color32(100, 100, 100, 255);

                if (!YandexGame.savesData.OpenLevels[selectedWorld, i])
                {
                    buttonLevels[i].GetComponent<Button>().enabled = false;
                    buttonLevels[i].GetComponent<Image>().color = new Color32(255, 255, 255, 150);
                    textLevels[i].color = new Color32(100, 100, 100, 150);
                }
            }
        }
    }

    public void OpenPanelFight()
    {
        UpdateWorldFightInfo();

        if (YandexGame.savesData.CurrentLearningStep == 46) { sampleSceneLearning.SetNewStep(); }

        bgWorldPanel.SetActive(true);
    }

    public void ClosePanelFight()
    {
        bgWorldPanel.SetActive(false);
    }

    public void StartLevel(int index)
    {
        SceneManager.LoadScene(worldFightScenes[selectedWorld]);
        PlayerStats.playerStats.LevelIndex = index;
    }
}
