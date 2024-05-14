using TMPro;
using UnityEngine;

public class ValueHit : MonoBehaviour
{
    [SerializeField] private float speedOfMoving;

    [SerializeField] private TextMeshPro textMeshPro;

    private void FixedUpdate()
    {
        gameObject.transform.position += new Vector3(0, speedOfMoving, 0) * Time.deltaTime;

        textMeshPro.color -= new Color32(0, 0, 0, 3);
    }
}
