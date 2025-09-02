using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnerManager : MonoBehaviour
{
    //[SerializeField] private ItemDrop[] powerUpRates;
    [SerializeField] private GameObject[] itemPrefabs;
    [SerializeField] private float chanceOfSpawn;

    public List<GameObject> allManagerSpawnedItems = new List<GameObject>();

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

    private void Start()
    {
        GameManager.Instance.OnStartMenu += DestroyAllManagerSpawnedItems;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnStartMenu -= DestroyAllManagerSpawnedItems;
    }

    public void TrySpawnItem(Vector3 spawnPosition, Quaternion spawnRotation)
    {
        if (itemPrefabs.Length < 1)
        {
            return;
        }
        if (Random.value <= chanceOfSpawn)
        {
            GameObject randomObject = itemPrefabs[Random.Range(0, itemPrefabs.Length)];
            Instantiate(randomObject, spawnPosition, spawnRotation);
        }
        // can do weighted chance
    }

    public void DestroyAllManagerSpawnedItems()
    {
        List<GameObject> allItems = new List<GameObject>(allManagerSpawnedItems);
        foreach (GameObject item in allItems)
        {
            if (item == null) continue;
            Destroy(item.gameObject);
        }

        allManagerSpawnedItems.Clear();
    }
}

//[System.Serializable]
//public class ItemDrop
//{
//    public GameObject item;
//    public float spawnRate;
//}