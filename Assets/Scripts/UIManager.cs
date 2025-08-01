using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private TextMeshProUGUI _numOfEnemiesText;

    Player _playerReference;

    void Awake()
    {
        _healthText.text = "100%";
        _scoreText.text = "0";
        _numOfEnemiesText.text = "0";

        _playerReference = FindAnyObjectByType<Player>();
    }

    private void Start()
    {
        _playerReference.health.OnHealthChange += UpdateHealthText;
        ScoreManager.Instance.OnScoreChange += UpdateScoreText;
        Enemy.OnAllSpawnedEnemiesChange += UpdateNumOfEnemiesText;
    }

    private void UpdateHealthText(int updatedHealthValue)
    {
        _healthText.text = updatedHealthValue.ToString() + "%";
    }

    private void UpdateScoreText(int updatedScoreValue)
    {
        _scoreText.text = updatedScoreValue.ToString();
    }

    private void UpdateNumOfEnemiesText(int numOfSpawnedEnemies)
    {
        _numOfEnemiesText.text = $"Enemies: {numOfSpawnedEnemies}";
    }
}
