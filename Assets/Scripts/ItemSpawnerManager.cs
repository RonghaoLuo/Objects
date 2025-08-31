using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnerManager : MonoBehaviour
{
    //[SerializeField] private ItemDrop[] powerUpRates;
    [SerializeField] private GameObject[] powerUpPrefabs;
    [SerializeField] private float chanceOfSpawn;

    public static ItemSpawnerManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There's another Item Spawner Manager as Instance");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void TrySpawnItem(Vector3 spawnPosition, Quaternion spawnRotation)
    {
        if (powerUpPrefabs.Length < 1)
        {
            return;
        }
        if (Random.value <= chanceOfSpawn)
        {
            GameObject randomObject = powerUpPrefabs[Random.Range(0, powerUpPrefabs.Length)];
            Instantiate(randomObject, spawnPosition, spawnRotation);
        }
        // can do weighted chance
    }
}

[System.Serializable]
public class ItemDrop
{
    public GameObject item;
    public float spawnRate;
}