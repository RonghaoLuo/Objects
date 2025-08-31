using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private int _maxNumOfEnemy;
    [SerializeField] private float _spawnCooldown = 1f;
    [SerializeField] private List<Enemy> _allManagerSpawnedEnemies = new List<Enemy>(); // use hashset?
    [SerializeField] private List<Transform> allSpawnPoints = new List<Transform>();
    [SerializeField] private Enemy _enemyPrefab;

    private Coroutine spawnEnemiesCoroutine;

    public static EnemyManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There's another Enemy Manager as Instance");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        GameManager.Instance.OnStartMenu += DestroyAllEnemy;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnStartMenu -= DestroyAllEnemy;
    }

    public void KillAllEnemy()
    {
        List<Enemy> allEnemies = new List<Enemy>(Enemy.allSpawnedEnemies);
        foreach (Enemy enemy in allEnemies)
        {
            if (enemy == null) continue;
            enemy.health.Kill();
        }
    }

    public void DestroyAllEnemy()
    {
        List<Enemy> allEnemies = new List<Enemy>(Enemy.allSpawnedEnemies);
        foreach (Enemy enemy in allEnemies)
        {
            if (enemy == null) continue;
            Destroy(enemy.gameObject);
        }

        Enemy.allSpawnedEnemies.Clear();
    }

    void SpawnSingleEnemy()
    {
        Enemy clonedEnemy = Instantiate(_enemyPrefab);
        Transform randomSpawnPoint = allSpawnPoints[Random.Range(0, allSpawnPoints.Count)];
        clonedEnemy.transform.position = randomSpawnPoint.position;
    }

    IEnumerator SpawnEnemiesCoroutine()
    {
        //OnGameStart?.Invoke();

        while (true)
        {
            if (Enemy.allSpawnedEnemies.Count < _maxNumOfEnemy)
            {
                SpawnSingleEnemy();
                yield return new WaitForSeconds(_spawnCooldown);
            }

            yield return null;                          // so doesn't stuck in the while true
        }
    }

    public void StartSpawnEnemiesCoroutine()
    {
        spawnEnemiesCoroutine = StartCoroutine(SpawnEnemiesCoroutine());
    }

    public void StopSpawnEnemiesCoroutine()
    {
        if (spawnEnemiesCoroutine != null)
        {
            StopCoroutine(spawnEnemiesCoroutine);
            spawnEnemiesCoroutine = null;
        }
    }
}
