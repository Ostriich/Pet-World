using UnityEngine;

public class OpenClosePanel : MonoBehaviour
{
    [SerializeField] private GameObject openPanel, closePanel;

    public void OpenPanel()
    {
        openPanel.SetActive(true);
    }

    public void ClosePanel()
    {
        closePanel.SetActive(false);
    }
}
