using UnityEngine;

public class Coin : MonoBehaviour, IPickable
{
    public int scoreToAdd;
    public float lifeSpan;

    public void PickUp()
    {
        // Add to a score manager
        Destroy(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
