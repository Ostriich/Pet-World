using UnityEngine;
using UnityEngine.UI;

public class BossAnimation : MonoBehaviour
{
    [SerializeField] private GameObject upLine, downLine, animContainer, redArea;
    public GameObject MonsterContainer;

    private bool animIsActive = false;
    private int multiplier = 1;

    private void FixedUpdate()
    {
        if (animIsActive)
        {
            upLine.GetComponent<RectTransform>().anchoredPosition += new Vector2(5, 0);
            downLine.GetComponent<RectTransform>().anchoredPosition -= new Vector2(5, 0);

            MonsterContainer.GetComponent<Image>().color += new Color32(2, 2, 2, 0);
            MonsterContainer.transform.localScale += new Vector3(multiplier / 100f, multiplier / 100f, 0);

            if (MonsterContainer.transform.localScale.x >= 1.1) { multiplier = -1; }
            if (MonsterContainer.transform.localScale.x <= 0.9) { multiplier = 1; }

            if (upLine.GetComponent<RectTransform>().anchoredPosition.x >= 500) { animIsActive = false; }
        }
        else
        {      
            if (animContainer.activeSelf)
            {
                MonsterContainer.transform.localScale = new Vector3(1,1,1);
                redArea.SetActive(false);
                animContainer.SetActive(false);
            }
        }
    }

    public void StartAnimation()
    {
        upLine.GetComponent<RectTransform>().anchoredPosition = new Vector2(-500, -50);
        downLine.GetComponent<RectTransform>().anchoredPosition = new Vector2(500, 150);

        redArea.SetActive(true);
        animContainer.SetActive(true);
        multiplier = 1;
        animIsActive = true;
        MonsterContainer.GetComponent<Image>().color = Color.black;
        MonsterContainer.SetActive(true);
    }
}
