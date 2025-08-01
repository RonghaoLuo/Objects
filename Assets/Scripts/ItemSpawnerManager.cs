using System.Linq;
using UnityEngine;

public class ItemSpawnerManager : MonoBehaviour
{
    //[SerializeField] private ItemDrop[] powerUpRates;
    [SerializeField] private GameObject[] powerUps;
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
        if (powerUps.Length < 1)
        {
            return;
        }
        if (Random.value <= chanceOfSpawn)
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