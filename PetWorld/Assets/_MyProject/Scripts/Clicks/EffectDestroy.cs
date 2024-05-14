using UnityEngine;

public class EffectDestroy : MonoBehaviour
{
    [SerializeField] private float lifeTime;

    private void Start()
    {
        Invoke("DestroyObject", lifeTime);
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
