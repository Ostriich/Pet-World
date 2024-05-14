using UnityEngine;
using UnityEngine.UI;

public class GetPetDailyReward : MonoBehaviour
{
    [SerializeField] private GameObject petRewardPanel;
    [SerializeField] private RectTransform whiteArea;
    [SerializeField] private Image petImage;
    [SerializeField] private RectTransform petScale;

    private bool animationStarted = false;

    public void StartAnimation()
    {
        animationStarted = true;
    }

    private void Update()
    {
        if (animationStarted)
        {
            whiteArea.localScale += new Vector3(0.2f, 0.2f, 0) * Time.deltaTime;
            petScale.localScale += new Vector3(0.2f, 0.2f, 0) * Time.deltaTime;
            petImage.color += new Color32(1, 1, 1, 0);

            if (whiteArea.localScale.x >= 1 )
            {
                animationStarted = false;
                Invoke("ClosePanel", 2);
            }
        }
    }

    private void ClosePanel()
    {
        whiteArea.localScale = new Vector3(0.2f, 0.2f, 1);
        petScale.localScale = new Vector3(0.2f, 0.2f, 1);
        petImage.color = Color.black;

        petRewardPanel.SetActive(false);
    }
}
