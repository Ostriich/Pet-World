using UnityEngine;
using UnityEngine.UI;
using YG;

public class SoundControl : MonoBehaviour
{
    [SerializeField] private Sprite buttonOn, buttonOff;
    [SerializeField] private Image buttonSound;

    private void Start()
    {
        foreach (AudioSource audio in FindObjectsOfType<AudioSource>())
        {
            audio.enabled = YandexGame.savesData.Sound;
        }

        //GetComponent<AudioListener>().enabled = YandexGame.savesData.Sound;

        if (buttonSound)
        {
            if (YandexGame.savesData.Sound) { buttonSound.sprite = buttonOn; }
            else { buttonSound.sprite = buttonOff; }
        }
    }

    public void ClickSound()
    {
        if (YandexGame.savesData.Sound) { buttonSound.sprite = buttonOff; }
        else { buttonSound.sprite = buttonOn; }

        YandexGame.savesData.Sound = !YandexGame.savesData.Sound;
        YandexGame.SaveProgress();

        //GetComponent<AudioListener>().enabled = YandexGame.savesData.Sound;

        foreach (AudioSource audio in FindObjectsOfType<AudioSource>())
        {
            audio.enabled = YandexGame.savesData.Sound;
        }
    }
}
