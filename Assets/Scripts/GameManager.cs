using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int _maxNumOfEnemy;
    [SerializeField] private List<Enemy> _allManagerSpawnedEnemies = new List<Enemy>(); // use hashset?
    [SerializeField] private List<Transform> allSpawnPoints = new List<Transform>();
    
    public static GameManager Instance;

    public Action OnGameStart, OnGameEnd;

    [SerializeField] private Enemy _enemyPrefab;

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
        OnGameEnd?.Invoke();
        ScoreManager.Instance.RegisterHighestScore();
    }

    void SpawnSingleEnemy()
    {
        Enemy clonedEnemy = Instantiate(_enemyPrefab);
        Transform randomSpawnPoint = 
            allSpawnPoints[UnityEngine.Random.Range(0, allSpawnPoints.Count)];
        clonedEnemy.transform.position = randomSpawnPoint.position;

        clonedEnemy.health.OnHealthZero += 
            (() => 
            {
                ItemSpawnerManager.Instance.TrySpawnItem(clonedEnemy.transform.position, clonedEnemy.transform.rotation);
            });
    }

    IEnumerator SpawnEnemiesCoroutine()
    {
        //OnGameStart?.Invoke();

        while (true)
        {
            if (Enemy.allSpawnedEnemies.Count < _maxNumOfEnemy)
            {
                SpawnSingleEnemy();
                yield return new WaitForSeconds(UnityEngine.Random.Range(2, 3f));
            }
            
            yield return null;                          // so doesn't stuck in the while true
        }
    }
}
