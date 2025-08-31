using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int _currentScore;
    [SerializeField] private int _highestScore;

    public static ScoreManager Instance;

    public Action<int> OnScoreChange;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There's another Score Manager as Instance");
            Destroy(gameObject);
            return;
        }
        Instance = this;

        GameManager.Instance.OnGameEnd += RegisterHighestScore;
    }

    void Start()
    {
        _highestScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    public void AddScore(int toAdd)
    {
        _currentScore += toAdd;
        OnScoreChange?.Invoke(_currentScore);
    }

    public void RegisterHighestScore()
    {
        if (_currentScore > _highestScore)
        {
            _highestScore = _currentScore;
            PlayerPrefs.SetInt("HighScore", _highestScore);
        }
    }
}
