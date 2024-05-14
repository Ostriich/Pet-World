using UnityEngine;

public class PlayClip : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip clip;

    public void PlayOneShot()
    {
        audioSource.PlayOneShot(clip);
    }
}
