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
