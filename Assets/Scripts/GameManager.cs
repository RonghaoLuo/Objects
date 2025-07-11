using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private List<Enemy> allSpawnedEnemies = new List<Enemy>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("SpawnEnemy", 1f, 1f);
    }

    void SpawnEnemy()
    {
        if (allSpawnedEnemies.Count >= 5)
        {
            CancelInvoke();
            return;
        }

        allSpawnedEnemies.Add(Instantiate(enemyPrefab));
    }
}
