using UnityEngine;

public class InstantiateValuePrefab : MonoBehaviour
{
    [SerializeField] private TextMesh valueText;
    public int Value = 0;

    public void UpdateValue()
    {
        valueText.text = "+" + Value.ToString();
    }
}
