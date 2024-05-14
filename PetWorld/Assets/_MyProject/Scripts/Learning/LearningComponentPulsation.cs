using UnityEngine;
using YG;

public class LearningComponentPulsation : MonoBehaviour
{
    [SerializeField] private int lastLearningIndex, firstLearningIndex;
    [SerializeField] private Animator anim;

    private void Update()
    {
        if (lastLearningIndex < YandexGame.savesData.CurrentLearningStep) { Destroy(anim); }
        gameObject.transform.localScale = new Vector3(1, 1, 1);

        if (firstLearningIndex >= YandexGame.savesData.CurrentLearningStep && anim) { anim.enabled = false; }
        else if (anim) {anim.enabled = true; }
    }
}
