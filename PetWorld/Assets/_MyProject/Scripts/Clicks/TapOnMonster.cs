using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using YG;

public class TapOnMonster : MonoBehaviour, IPointerClickHandler
{
    public int hitValue, criticalChance;
    public GameObject HitEffect, CritEffect, hitValueText, critValueText;

    public bool MonsterHasHP;
    public MonsterHP Monster;
    [SerializeField] private EggTapAnimation MonsterTapAnimation;

    [SerializeField] private PlayClip playerHitSound, playerCritSound;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (MonsterHasHP)
        {
            MonsterTapAnimation.Multiplier = -1;

            // Count probability of Critical damage
            int randomChance = Random.Range(0, 100);

            if (randomChance < criticalChance)
            {
                Monster.CurrentHealth -= hitValue * 3;
                SpawnEffect(CritEffect, critValueText, hitValue * 3);

                playerCritSound.PlayOneShot();
            }
            else
            {
                Monster.CurrentHealth -= hitValue;
                SpawnEffect(HitEffect, hitValueText, hitValue);

                playerHitSound.PlayOneShot();
            }
        }
    }

    private void SpawnEffect(GameObject effect, GameObject valueText, int valueHit)
    {
        // Determine the point of spawn
        int angle = Random.Range(0, 359);
        float radius = Random.Range(10, 40);
        radius /= 10;
        float x = Mathf.Cos(angle) * radius;
        float y = Mathf.Sin(angle) * radius;

        // Spawn the clickSpirit
        Instantiate(effect, new Vector3(x, y, 0), Quaternion.identity);

        GameObject clickText = Instantiate(valueText, new Vector3(x, y, 0), Quaternion.identity);
        clickText.GetComponent<TextMeshPro>().text = ShortCutValue.shortCutValue.CutIntValue(valueHit, 2);
    }
}
