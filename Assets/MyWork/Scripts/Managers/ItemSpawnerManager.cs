using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnerManager : MonoBehaviour
{
    //[SerializeField] private ItemDrop[] powerUpRates;
    [SerializeField] private GameObject[] itemPrefabs;
    [SerializeField] private List<GameObject> weaponDropPrefabs;
    [SerializeField] private float itemSpawnChance;
    [SerializeField] private float itemDespawnTime;

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

    public void TrySpawnWeaponDrop(Vector3 spawnPosition)
    {
        if (weaponDropPrefabs.Count < 1)
        {
            return;
        }
        
        GameObject randomWeapon = weaponDropPrefabs[Random.Range(0, weaponDropPrefabs.Count)];
        GameObject weaponDrop = Instantiate(randomWeapon, spawnPosition, Quaternion.identity);
    }

    public void TrySpawnItem(Vector3 spawnPosition)
    {
        if (itemPrefabs.Length < 1)
        {
            return;
        }
        if (Random.value <= itemSpawnChance)
        {
            GameObject randomObject = itemPrefabs[Random.Range(0, itemPrefabs.Length)];
            GameObject item = Instantiate(randomObject, spawnPosition, Quaternion.identity);

            Destroy(item, itemDespawnTime);
            // can also do weighted chance
        }
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