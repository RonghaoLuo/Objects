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
        StartCoroutine(SpawnEnemiesCoroutine());
    }

    void SpawnSingleEnemy()
    {
        allSpawnedEnemies.Add(Instantiate(enemyPrefab));
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
