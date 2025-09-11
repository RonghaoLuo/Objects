using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private int maxNumOfEnemy;
    [SerializeField] private int currentMaxNumOfEnemy;
    [SerializeField] private int currentMaxNumOfStations;
    [SerializeField] private float _spawnCooldown = 1f;
    //[SerializeField] private List<Enemy> _allManagerSpawnedEnemies = new List<Enemy>(); // use hashset?
    [SerializeField] private List<Transform> allSpawnPoints = new List<Transform>();
    [SerializeField] private float stationSpawnMin, stationSpawnMax;
    [SerializeField] private GameObject[] _enemyPrefabs;
    [SerializeField] private GameObject[] enemyStationPrefabs;

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

        currentMaxNumOfEnemy = 2;
    }

    private void Start()
    {
        GameManager.Instance.OnStartMenu += DestroyAllEnemy;
        GameManager.Instance.OnGameEnd += ResetMaxNumOfEnemy;
        GameManager.Instance.OnGameStart += ResetMaxNumOfEnemy;

        //SpawnEnemyStation();
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnStartMenu -= DestroyAllEnemy;
        GameManager.Instance.OnGameEnd -= ResetMaxNumOfEnemy;
        GameManager.Instance.OnGameStart -= ResetMaxNumOfEnemy;
    }

    private void Update()
    {
        if (currentMaxNumOfEnemy < maxNumOfEnemy)
        {
            currentMaxNumOfEnemy = 2 + 2 * ScoreManager.Instance.GetCurrentScore() / 10;
        }
        else
        {
            currentMaxNumOfEnemy = maxNumOfEnemy;
        }
    }

    public void NukeAllEnemy()
    {
        List<Enemy> allEnemies = new List<Enemy>(Enemy.allSpawnedEnemies);
        foreach (Enemy enemy in allEnemies)
        {
            if (enemy == null) continue;
            enemy.health.Damage((int)(enemy.health.GetMaxHealth() * (1 - enemy.GetNukeResistanceFraction())));
        }

        List<StationEnemy> allStations = new List<StationEnemy>(Enemy.allSpawnedStations);
        foreach (StationEnemy station in allStations)
        {
            if (station == null) continue;
            station.health.Damage((int)(station.health.GetMaxHealth() * (1 - station.GetNukeResistanceFraction())));
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

        List<StationEnemy> allStations = new List<StationEnemy>(Enemy.allSpawnedStations);
        foreach (Enemy station in allStations)
        {
            if (station == null) continue;
            Destroy(station.gameObject);
        }

        Enemy.allSpawnedEnemies.Clear();
        Enemy.allSpawnedStations.Clear();
    }

    void SpawnSingleEnemy()
    {
        GameObject randomEnemyToSpawn = _enemyPrefabs[Random.Range(0, _enemyPrefabs.Length)];

        GameObject clonedEnemy = Instantiate(randomEnemyToSpawn);
        Transform randomSpawnPoint = allSpawnPoints[Random.Range(0, allSpawnPoints.Count)];
        clonedEnemy.transform.position = randomSpawnPoint.position;
    }

    IEnumerator SpawnEnemiesCoroutine()
    {
        //OnGameStart?.Invoke();

        while (true)
        {
            if (Enemy.allSpawnedEnemies.Count < currentMaxNumOfEnemy)
            {
                SpawnSingleEnemy();
                yield return new WaitForSeconds(_spawnCooldown);
            }
            if (Enemy.allSpawnedStations.Count < currentMaxNumOfStations)
            {
                SpawnEnemyStation();
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

    /// <summary>
    /// Resets the CurrentMaxNumOfEnemy to 2.
    /// </summary>
    private void ResetMaxNumOfEnemy()
    {
        currentMaxNumOfEnemy = 2;
    }

    private void SpawnEnemyStation()
    {
        GameObject randomStationToSpawn = enemyStationPrefabs[Random.Range(0, enemyStationPrefabs.Length)];

        GameObject clonedStation = Instantiate(randomStationToSpawn);
        Vector2 randomSpawnPoint = new Vector2();
        randomSpawnPoint.x = Random.Range(stationSpawnMin, stationSpawnMax);
        randomSpawnPoint.y = Random.Range(stationSpawnMin, stationSpawnMax);
        clonedStation.transform.position = randomSpawnPoint;
    }
}
