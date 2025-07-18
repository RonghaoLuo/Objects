using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int currentScore;
    [SerializeField] private int highestScore;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        highestScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore(int toAdd)
    {
        currentScore += toAdd;
    }

    public void RegisterHighestScore()
    {
        if (currentScore > highestScore)
        {
            highestScore = currentScore;
            PlayerPrefs.SetInt("HighScore", highestScore);
        }
    }
}
