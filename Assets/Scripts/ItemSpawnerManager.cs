using System.Linq;
using UnityEngine;

public class ItemSpawnerManager : MonoBehaviour
{
    [SerializeField] private ItemDrop[] powerUpRates;
    [SerializeField] private GameObject[] powerUps;
    [SerializeField] private float chanceOfSpawn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TrySpawnItem(Vector3 spawnPosition, Quaternion spawnRotation)
    {
        if (Random.value < chanceOfSpawn)       // chance?
        {
            GameObject randomObject = powerUps[Random.Range(0, powerUps.Length)];
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