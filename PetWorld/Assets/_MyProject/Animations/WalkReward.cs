using UnityEngine;

public class WalkReward : MonoBehaviour
{
    [SerializeField] private float speedOfMoving;

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private TextMesh textMesh;

    private void FixedUpdate()
    {
        gameObject.transform.position += new Vector3(0, speedOfMoving, 0) * Time.deltaTime;

        spriteRenderer.color -= new Color32(0, 0, 0, 3);
        textMesh.color -= new Color32(0, 0, 0, 3);
    }
}
