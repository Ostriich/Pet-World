using UnityEngine;
using UnityEngine.UI;

public class NewLevelAnimation : MonoBehaviour
{
    [SerializeField] private GameObject upContainer;
    [SerializeField] private Image[] imageObjects;
    [SerializeField] private Text textNewLevel;
    [SerializeField] private Color32 startColorText;

    private bool animIsActive = false;

    private void FixedUpdate()
    {
        if (animIsActive)
        {
            upContainer.GetComponent<RectTransform>().anchoredPosition += new Vector2(0, 4);

            foreach (Image img in imageObjects)
            {
                img.color -= new Color32(0, 0, 0, 3);
            }

            textNewLevel.color -= new Color32(0, 0, 0, 3);

            if (textNewLevel.color.a <= 0) { animIsActive = false; }
        }
        else
        {
            upContainer.SetActive(false);
        }
    }

    public void StartAnimation()
    {
        upContainer.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -100);
        foreach (Image img in imageObjects)
        {
            img.color = new Color32(255, 255, 255, 255);
        }

        textNewLevel.color = startColorText;

        upContainer.SetActive(true);
        animIsActive = true;
    }
}
