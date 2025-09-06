using UnityEngine;

public class Coin : MonoBehaviour, IPickable
{
    public int scoreToAdd;
    public float lifeSpan;

    public void PickUp()
    {
        ScoreManager.Instance.AddScore(scoreToAdd);
        Destroy(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ItemSpawnerManager.Instance.allManagerSpawnedItems.Add(gameObject);
    }

    private void OnDestroy()
    {
        ItemSpawnerManager.Instance.allManagerSpawnedItems.Remove(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
