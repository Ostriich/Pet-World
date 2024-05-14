using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class MonsterAttack : MonoBehaviour
{
    // UI

    [SerializeField] private Image attackLine;
    [SerializeField] private GameObject damagePrefab, effectPrefab;
    public GameObject RedEffect;

    // Constants

    public int DamageValue;
    public float AttackCooldown;
    public bool IsAttack = false;
    public float timer;

    [SerializeField] private PlayClip monsterSound;

    public void StartAttack()
    {
        timer = 0;
        IsAttack = true;
    }

    private void FixedUpdate()
    {
        if (IsAttack && YandexGame.savesData.CurrentLearningStep > 52) { timer += Time.deltaTime; }
    }

    private void Update()
    {
        attackLine.fillAmount = timer / AttackCooldown;

        if (timer >= AttackCooldown)
        {
            timer = 0;
            Attack();
        }

        if (timer >= 0.25) { RedEffect.SetActive(false); }
    }

    private void Attack()
    {
        PlayerStats.playerStats.CurrentHealthPlayer -= DamageValue;
        RedEffect.SetActive(true);

        Instantiate(effectPrefab);

        GameObject damagePref = Instantiate(damagePrefab);
        damagePref.GetComponent<TextMeshPro>().text = ShortCutValue.shortCutValue.CutIntValue(DamageValue, 2);

        monsterSound.PlayOneShot();
    }
}
