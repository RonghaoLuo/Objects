using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI numOfEnemiesText;
    [SerializeField] private GameObject[] nukeIcons;
    [SerializeField] private TextMeshProUGUI endMenuScore;
    [SerializeField] private TextMeshProUGUI endMenuHighestScore;
    [SerializeField] private TextMeshProUGUI startMenuHighestScore;

    Player player;

    void Awake()
    {
        healthText.text = "N/A";
        scoreText.text = "N/A";
        numOfEnemiesText.text = "N/A";
    }

    private void Start()
    {
        GameManager.Instance.OnPlayerSpawn += SetPlayerReference;
        ScoreManager.Instance.OnScoreChange += UpdateScoreText;
        Enemy.OnAllSpawnedEnemiesChange += UpdateNumOfEnemiesText;
        GameManager.Instance.OnGameEnd += UpdateEndGameMenu;
        GameManager.Instance.OnStartMenu += UpdateStartMenu;

        UpdateStartMenu();
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnPlayerSpawn -= SetPlayerReference;
        ScoreManager.Instance.OnScoreChange -= UpdateScoreText;
        Enemy.OnAllSpawnedEnemiesChange -= UpdateNumOfEnemiesText;
        GameManager.Instance.OnGameEnd -= UpdateEndGameMenu;
        GameManager.Instance.OnStartMenu -= UpdateStartMenu;
    }

    private void UpdateHealthText()
    {
        if (player != null) 
            healthText.text = player.health.GetHealth().ToString() + "%";
    }

    private void UpdateScoreText(int updatedScoreValue)
    {
        scoreText.text = updatedScoreValue.ToString();
        Debug.Log("score updated, current score is: " + updatedScoreValue);
    }

    private void UpdateNumOfEnemiesText(int numOfSpawnedEnemies)
    {
        numOfEnemiesText.text = $"Enemies: {numOfSpawnedEnemies}";
    }

    private void UpdateEndGameMenu()
    {
        endMenuScore.text = ScoreManager.Instance.GetCurrentScore().ToString();
        endMenuHighestScore.text = ScoreManager.Instance.GetHighestScore().ToString();
    }

    private void UpdateStartMenu()
    {
        startMenuHighestScore.text = "Highest Score: " + ScoreManager.Instance.GetHighestScore().ToString();
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
        player.health.OnHealthChange -= UpdateHealthText;
        player.OnNukeChange -= UpdateNuke;
        player.health.OnHealthZero -= RemovePlayerReference;

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
