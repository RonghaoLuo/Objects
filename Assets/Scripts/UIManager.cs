using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _healthText;

    Player _playerReference;

    void Awake()
    {
        _healthText.text = "100%";
        _scoreText.text = "0";

        _playerReference = FindAnyObjectByType<Player>();
    }

    private void Start()
    {
        _playerReference.health.OnHealthChange += UpdateHealthText;
        ScoreManager.Instance.OnScoreChange += UpdateScoreText;
    }

    private void UpdateHealthText(int updatedHealthValue)
    {
        _healthText.text = updatedHealthValue.ToString() + "%";
    }

    private void UpdateScoreText(int updatedScoreValue)
    {
        _scoreText.text = updatedScoreValue.ToString();
    }
}
