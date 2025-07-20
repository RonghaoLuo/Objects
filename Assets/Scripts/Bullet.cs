using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _myRigidbody;
    //public WeaponData weaponOrigin;
    public float speed;
    public int damage;

    void Start()
    {
        Destroy(gameObject, 5f);
        _myRigidbody.linearVelocity = transform.up * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Character>())  // unity auto converts to bool
        {
            collision.gameObject.GetComponent<Character>().health.Damage(damage);
        }
        Destroy(gameObject);
    }
}
