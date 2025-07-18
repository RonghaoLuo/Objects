using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private List<Enemy> allSpawnedEnemies = new List<Enemy>();

    [SerializeField] private UnityEvent OnGameStart;

    public static GameManager Instance;

    private ScoreManager scoreManager;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There's another Game Manager as Instance");
            Destroy(gameObject);
            return;
        }
        Instance = this;

        scoreManager = FindAnyObjectByType<ScoreManager>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FindAnyObjectByType<Player>().health.OnHealthZero += EndGame;
        StartCoroutine(SpawnEnemiesCoroutine());
    }

    void EndGame()
    {
        Debug.LogWarning("Game Over");
        scoreManager.RegisterHighestScore();
    }

    void SpawnSingleEnemy()
    {
        Enemy clonedEnemy = Instantiate(enemyPrefab);
        allSpawnedEnemies.Add(clonedEnemy);
        clonedEnemy.health.OnHealthZero += 
            (() => 
            {
                scoreManager.AddScore(10);
                allSpawnedEnemies.Remove(clonedEnemy);
                Debug.Log(clonedEnemy.name + " got killed");
                Destroy(clonedEnemy.gameObject); 
            });
    }

    IEnumerator SpawnEnemiesCoroutine()
    {
        while (allSpawnedEnemies.Count < 15)
        {
            SpawnSingleEnemy();
            yield return new WaitForSeconds(Random.Range(2, 3f));
        }

        OnGameStart.Invoke();
    }
}
