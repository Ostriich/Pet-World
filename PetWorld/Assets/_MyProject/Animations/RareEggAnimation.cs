using UnityEngine;

public class RareEggAnimation : MonoBehaviour
{
    [SerializeField] private GameObject upLine, downLine, rareEggPrefab, animContainer;

    private bool animIsActive = false;
    private bool prefabIsInst = false;

    private void FixedUpdate()
    {
        if (animIsActive)
        {
            upLine.GetComponent<RectTransform>().anchoredPosition += new Vector2(5, 0);
            downLine.GetComponent<RectTransform>().anchoredPosition -= new Vector2(5, 0);

            if (upLine.GetComponent<RectTransform>().anchoredPosition.x >= -400 && !prefabIsInst) 
            { 
                Instantiate(rareEggPrefab);
                prefabIsInst = true;
            }

            if (upLine.GetComponent<RectTransform>().anchoredPosition.x >= 500) { animIsActive = false; }
        }
        else
        {
            animContainer.SetActive(false);
        }
    }

    public void StartAnimation()
    {
        upLine.GetComponent<RectTransform>().anchoredPosition = new Vector2(-500, -50);
        downLine.GetComponent<RectTransform>().anchoredPosition = new Vector2(500, 150);

        animContainer.SetActive(true);
        prefabIsInst = false;
        animIsActive = true;
    }
}
