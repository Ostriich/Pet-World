using UnityEngine;

public class ButtonGoToIsland : MonoBehaviour
{
    [SerializeField] private GameObject[] island = new GameObject[5];
    [SerializeField] private ChooseFightPanel chooseFightPanel;

    [SerializeField] private RectTransform rectTransform;

    private void Update()
    {
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, island[chooseFightPanel.selectedWorld].transform.position.y * 100);
    }
}
