using UnityEngine;

public class Projectile : MonoBehaviour, IDespawnable
{
    private int _despawnTime = 5;
    private Health _playerHealth;

    public void Despawn(float time)
    {
        Invoke("DestroyThis", time);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Despawn(_despawnTime);
        _playerHealth = FindAnyObjectByType<Player>().health;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DestroyThis()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _playerHealth.Damage(1);
            Destroy(gameObject);
        }
    }
}
