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

        OnScoreChange += RegisterHighestScore;
        GameManager.Instance.OnGameStart += ResetScore;
    }

    void Start()
    {
        _highestScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    private void OnDestroy()
    {
        OnScoreChange -= RegisterHighestScore;
        GameManager.Instance.OnGameStart -= ResetScore;
    }

    public void AddScore(int toAdd)
    {
        _currentScore += toAdd;
        OnScoreChange?.Invoke(_currentScore);
    }

    public void RegisterHighestScore(int newHighestScore)
    {
        if (newHighestScore > _highestScore)
        {
            _highestScore = newHighestScore;
            PlayerPrefs.SetInt("HighScore", _highestScore);
        }
    }

    private void ResetScore()
    {
        _currentScore = 0;
        OnScoreChange?.Invoke(_currentScore);
        Debug.Log("score is reset");
    }

    public int GetCurrentScore()
    {
        return _currentScore;
    }

    public int GetHighestScore()
    {
        return _highestScore;
    }
}
