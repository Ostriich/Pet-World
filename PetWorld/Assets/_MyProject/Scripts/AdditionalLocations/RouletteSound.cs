using UnityEngine;

public class RouletteSound : MonoBehaviour
{
    [SerializeField] private PlayClip rouletteTic;

    public bool alreadyPlayed;

    private void Update()
    {
        if (gameObject.transform.position.x < -1 && !alreadyPlayed)
        {
            alreadyPlayed = true;
            rouletteTic.PlayOneShot();
        }
    }
}
