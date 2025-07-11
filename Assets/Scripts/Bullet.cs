using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D myRigidbody;
    public WeaponData weaponOrigin;
    public float speed;
    public int damage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, 5f);
        myRigidbody.linearVelocity = transform.up * speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Character>())  // unity auto converts to bool
        {
            collision.gameObject.GetComponent<Character>().health.Damage(10);
        }
        Destroy(gameObject);
    }
}
