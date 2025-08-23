using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private TextMeshProUGUI _numOfEnemiesText;
    [SerializeField] private GameObject[] _nukeIcons;

    Player player;

    void Awake()
    {
        _healthText.text = "100%";
        _scoreText.text = "0";
        _numOfEnemiesText.text = "0";

        player = FindAnyObjectByType<Player>();
    }

    private void Start()
    {
        player.health.OnHealthChange += UpdateHealthText;
        ScoreManager.Instance.OnScoreChange += UpdateScoreText;
        Enemy.OnAllSpawnedEnemiesChange += UpdateNumOfEnemiesText;
        player.OnNukeChange += UpdateNuke;
    }

    private void UpdateHealthText()
    {
        _healthText.text = player.health.GetHealth().ToString() + "%";
    }

    private void UpdateScoreText(int updatedScoreValue)
    {
        _scoreText.text = updatedScoreValue.ToString();
    }

    private void UpdateNumOfEnemiesText(int numOfSpawnedEnemies)
    {
        _numOfEnemiesText.text = $"Enemies: {numOfSpawnedEnemies}";
    }

    private void UpdateNuke()
    {
        int numOfNukes = player.numOfNukes;
        for (int i = 0; i < _nukeIcons.Length; i++)
        {
            if (i < numOfNukes)
            {
                _nukeIcons[i].SetActive(true);
            }
            else
            {
                _nukeIcons[i].SetActive(false);
            }
        }
    }
}
