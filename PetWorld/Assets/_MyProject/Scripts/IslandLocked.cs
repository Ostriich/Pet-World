using UnityEngine;
using YG;

public class IslandLocked : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] colorObjects;
    [SerializeField] private Animator[] hideAnim;
    [SerializeField] private GameObject[] hideObjects;
    [SerializeField] private GameObject[] showObjects;
    [SerializeField] private int islandIndex;

    private void Start()
    {
        UpdateVisibilityIsland();
    }

    public void UpdateVisibilityIsland()
    {
        if (!YandexGame.savesData.OpenLocations[islandIndex])
        {
            foreach (SpriteRenderer obj in colorObjects) { obj.color = Color.black; }
            foreach (Animator obj in hideAnim) { obj.enabled = false; }
            foreach (GameObject obj in hideObjects) { obj.SetActive(false); }
            foreach (GameObject obj in showObjects) { obj.SetActive(true); }
        }
        else
        {
            foreach (SpriteRenderer obj in colorObjects) { obj.color = Color.white; }
            foreach (Animator obj in hideAnim) { obj.enabled = true; }
            foreach (GameObject obj in hideObjects) { obj.SetActive(true); }
            foreach (GameObject obj in showObjects) { obj.SetActive(false); }
        }
    }
}
