using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int _maxNumOfEnemy;
    [SerializeField] private List<Enemy> _allSpawnedEnemies = new List<Enemy>();
    
    public static GameManager Instance;

    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private UnityEvent OnGameStart;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There's another Game Manager as Instance");
            Destroy(gameObject);
            return;
        }
        Instance = this;
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
        ScoreManager.Instance.RegisterHighestScore();
    }

    void SpawnSingleEnemy()
    {
        Enemy clonedEnemy = Instantiate(_enemyPrefab);
        _allSpawnedEnemies.Add(clonedEnemy);
        clonedEnemy.health.OnHealthZero += 
            (() => 
            {
                ScoreManager.Instance.AddScore(10);
                _allSpawnedEnemies.Remove(clonedEnemy);
                Debug.Log(clonedEnemy.name + " got killed");
                Destroy(clonedEnemy.gameObject); 
            });
    }

    IEnumerator SpawnEnemiesCoroutine()
    {
        while (_allSpawnedEnemies.Count < _maxNumOfEnemy)
        {
            SpawnSingleEnemy();
            yield return new WaitForSeconds(Random.Range(2, 3f));
        }

        OnGameStart.Invoke();
    }
}
