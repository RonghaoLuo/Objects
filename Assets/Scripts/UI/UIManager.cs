using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI numOfEnemiesText;
    [SerializeField] private GameObject[] nukeIcons;

    Player player;

    void Awake()
    {
        healthText.text = "100%";
        scoreText.text = "0";
        numOfEnemiesText.text = "0";

        //player = FindAnyObjectByType<Player>();
        GameManager.Singleton.OnPlayerSpawn += SetPlayerReference;
        //GameManager.Singleton.OnGameEnd += RemovePlayerReference;
    }

    private void Start()
    {
        //player.health.OnHealthChange += UpdateHealthText;
        ScoreManager.Instance.OnScoreChange += UpdateScoreText;
        Enemy.OnAllSpawnedEnemiesChange += UpdateNumOfEnemiesText;
        //player.OnNukeChange += UpdateNuke;
    }

    private void UpdateHealthText()
    {
        if (player != null) 
            healthText.text = player.health.GetHealth().ToString() + "%";
    }

    private void UpdateScoreText(int updatedScoreValue)
    {
        scoreText.text = updatedScoreValue.ToString();
    }

    private void UpdateNumOfEnemiesText(int numOfSpawnedEnemies)
    {
        numOfEnemiesText.text = $"Enemies: {numOfSpawnedEnemies}";
    }

    private void UpdateNuke()
    {
        int numOfNukes = player.numOfNukes;
        for (int i = 0; i < nukeIcons.Length; i++)
        {
            if (i < numOfNukes)
            {
                nukeIcons[i].SetActive(true);
            }
            else
            {
                nukeIcons[i].SetActive(false);
            }
        }
    }
    private void SetPlayerReference(Player player)
    {
        this.player = player;
        LinkUIValuesToPlayer();
    }

    private void RemovePlayerReference()
    {
        player = null;

        ResetUIValues();
    }

    private void LinkUIValuesToPlayer()
    {
        player.health.OnHealthChange += UpdateHealthText;
        player.OnNukeChange += UpdateNuke;
        player.health.OnHealthZero += RemovePlayerReference;

        // do i need to update on the first time?
        UpdateHealthText();
        UpdateNuke();
    }

    private void ResetUIValues()
    {
        healthText.text = "N/A";

        for (int i = 0; i < nukeIcons.Length; i++)
        {
            nukeIcons[i].SetActive(false);
        }
    }
}
