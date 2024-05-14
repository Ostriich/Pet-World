using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class LoseGame : MonoBehaviour
{
    // Components

    [SerializeField] private MonsterAttack monsterAttack;
    [SerializeField] private GameObject tapPanel;
    [SerializeField] private GameObject gameOverPanel, skyPanel;

    private bool isLose = false;

    [SerializeField] private PlayClip loseSound;

    private void Update()
    {
        if (PlayerStats.playerStats.CurrentHealthPlayer <= 0 && !isLose)
        {
            isLose = true;
            Lose();
        }
    }

    private void Lose()
    {
        PlayerStats.playerStats.CurrentHealthPlayer = 0;
        tapPanel.SetActive(false);
        monsterAttack.IsAttack = false;
        gameOverPanel.SetActive(true);

        Invoke("LoadSky", 3);
        Invoke("LoadSampleScene", 5);

        loseSound.PlayOneShot();
    }

    private void LoadSky()
    {
        skyPanel.SetActive(true);
        PlayerStats.playerStats.UpdateStats();
    }

    private void LoadSampleScene()
    {
        SceneManager.LoadScene("_MyProject/Scenes/SampleScene");
    }
}
