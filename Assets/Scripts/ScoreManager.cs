using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int _currentScore;
    [SerializeField] private int _highestScore;

    public static ScoreManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There's another Score Manager as Instance");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        _highestScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    public void AddScore(int toAdd)
    {
        _currentScore += toAdd;
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
