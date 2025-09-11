using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI numOfEnemiesText;
    [SerializeField] private TextMeshProUGUI numOfStationsText;
    [SerializeField] private Image[] nukeIcons;
    [SerializeField] private TextMeshProUGUI endMenuScore;
    [SerializeField] private TextMeshProUGUI endMenuHighestScore;
    [SerializeField] private TextMeshProUGUI startMenuHighestScore;
    [SerializeField] private List<TextMeshProUGUI> weaponUIs;

    Player player;
    private List<bool> WeaponActivity;
    private int currentWeaponIndex;

    public static UIManager Instance;

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There's another UI Manager as Instance");
            Destroy(gameObject);
            return;
        }
        Instance = this;

        healthText.text = "N/A";
        scoreText.text = "N/A";
        numOfEnemiesText.text = "N/A";
    }

    private void Start()
    {
        GameManager.Instance.OnPlayerSpawn += SetPlayerReference;
        ScoreManager.Instance.OnScoreChange += UpdateScoreText;
        Enemy.OnAllSpawnedEnemiesChange += UpdateNumOfEnemiesText;
        Enemy.OnAllSpawnedStationsChange += UpdateNumOfStationsText;
        GameManager.Instance.OnGameEnd += UpdateEndGameMenu;
        GameManager.Instance.OnStartMenu += UpdateStartMenu;

        UpdateStartMenu();
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnPlayerSpawn -= SetPlayerReference;
        ScoreManager.Instance.OnScoreChange -= UpdateScoreText;
        Enemy.OnAllSpawnedEnemiesChange -= UpdateNumOfEnemiesText;
        Enemy.OnAllSpawnedStationsChange -= UpdateNumOfStationsText;
        GameManager.Instance.OnGameEnd -= UpdateEndGameMenu;
        GameManager.Instance.OnStartMenu -= UpdateStartMenu;
    }

    private void Update()
    {
        if (player != null)
        {
            WeaponActivity = player.GetWeaponActivity();
            currentWeaponIndex = player.GetCurrentWeaponIndex();

            for (int i = 0; i < WeaponActivity.Count; i++)
            {
                if (i == currentWeaponIndex)
                {
                    weaponUIs[i].color = Color.yellow;
                }
                else if (i != currentWeaponIndex && WeaponActivity[i])
                {
                    weaponUIs[i].color = Color.white;
                }
                else
                {
                    weaponUIs[i].color = Color.gray3;
                }
            }
        }
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

    private void UpdateNumOfStationsText(int numOfSpawnedStations)
    {
        numOfStationsText.text = $"Stations: {numOfSpawnedStations}";
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
                nukeIcons[i].color = Color.white;
            }
            else
            {
                nukeIcons[i].color = Color.gray1;
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
            nukeIcons[i].color = Color.gray1;
        }

        weaponUIs[0].color = Color.yellow;
        for (int i = 1; i < weaponUIs.Count; i++)
        {
            weaponUIs[i].color = Color.gray3;
        }
    }

    //public void SetWeaponUIColour(int weaponIndex, Color colour)
    //{
    //    weaponUIs[weaponIndex].color = colour;
    //}
}
