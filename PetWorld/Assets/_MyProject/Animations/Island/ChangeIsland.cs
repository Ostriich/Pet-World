using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class ChangeIsland : MonoBehaviour
{
    //Islands
    private int numOfIsland = 0;
    [SerializeField] private GameObject[] islandsContainers;
    [SerializeField] private GameObject[] namesOfIslands;

    // Canvas
    [SerializeField] private GameObject frontCanvas;
    [SerializeField] private Image backGround;
    [SerializeField] private Color32[] bgColors;
    [SerializeField] private GameObject buttonLeft, buttonRight;
    [SerializeField] private GameObject[] lockedTexts;

    // Moving Camera
    [SerializeField] private float smoothing = 5f;
    Vector3 offset = new Vector3(0,0,0);
    [SerializeField] private float timeColor;

    // process objects

    [SerializeField] private ChooseFightPanel chooseFightPanel;
    [SerializeField] private GameObject buttonsOnIsland;
    [SerializeField] private Image eggbutton, campainButton;
    [SerializeField] private Sprite[] eggSprites, campainSprites;

    [SerializeField] private string[] eggWorlds = new string[5];

    private void Start()
    {
        if (YandexGame.savesData.Level >= 15 && !YandexGame.savesData.OpenLocations[1]) { YandexGame.savesData.OpenLocations[1] = true; }
        if (YandexGame.savesData.Level >= 30 && YandexGame.savesData.CompletedLevels[0,9] && !YandexGame.savesData.OpenLocations[2]) { YandexGame.savesData.OpenLocations[2] = true; }
        if (YandexGame.savesData.Level >= 50 && YandexGame.savesData.CompletedLevels[1, 9] && !YandexGame.savesData.OpenLocations[3]) { YandexGame.savesData.OpenLocations[3] = true; }
        if (YandexGame.savesData.Level >= 70 && YandexGame.savesData.CompletedLevels[2, 9] && !YandexGame.savesData.OpenLocations[4]) { YandexGame.savesData.OpenLocations[4] = true; }

        numOfIsland = YandexGame.savesData.OpenIslandOnSampleScene;
        ChangeIslandClick(0);
    }

    void FixedUpdate()
    {
        if (Mathf.Abs(transform.position.x - offset.x) > 0.05)
        {
            frontCanvas.SetActive(false);
            transform.position = Vector3.Lerp(transform.position, offset, smoothing * Time.deltaTime);
            backGround.color = Color.Lerp(backGround.color, bgColors[numOfIsland], timeColor);
        }
        else
        {
            transform.position = offset;
            frontCanvas.SetActive(true);
            for (int i = 0; i < islandsContainers.Length; i++)
            {
                if (numOfIsland != i) 
                { 
                    islandsContainers[i].SetActive(false);
                    namesOfIslands[i].SetActive(false);
                }
            }

            if (YandexGame.savesData.OpenLocations[numOfIsland])
            {
                if (numOfIsland != 0)
                {
                    lockedTexts[numOfIsland - 1].SetActive(false);
                }

                buttonsOnIsland.SetActive(true);
            }
            else
            {
                lockedTexts[numOfIsland - 1].SetActive(true);
                buttonsOnIsland.SetActive(false);
            }

            namesOfIslands[numOfIsland].SetActive(true);

            if (YandexGame.savesData.CurrentLearningStep >= 63)
            {
                buttonLeft.SetActive(numOfIsland != 0);
                buttonRight.SetActive(numOfIsland != islandsContainers.Length - 1);
            }
        }
    }

    public void ChangeIslandClick(int delta)
    {
        frontCanvas.SetActive(false);
        foreach (GameObject obj in lockedTexts) { obj.SetActive(false); }
        numOfIsland += delta;

        eggbutton.sprite = eggSprites[numOfIsland];
        campainButton.sprite = campainSprites[numOfIsland];

        if (YandexGame.savesData.OpenLocations[numOfIsland])
        {
            YandexGame.savesData.OpenIslandOnSampleScene = numOfIsland;
            YandexGame.SaveProgress();
        }

        offset = new Vector3(numOfIsland * (-20), 0, 0);
        islandsContainers[numOfIsland].SetActive(true);

        chooseFightPanel.selectedWorld = numOfIsland;
        chooseFightPanel.UpdateWorldFightInfo();
    }

    public void OpenEggWorld()
    {
        SceneManager.LoadScene(eggWorlds[numOfIsland]);
    }
}
