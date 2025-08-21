using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _myRigidbody;
    [SerializeField] private GameObject _destroyEffect;
    
    //public WeaponData weaponOrigin;
    public float speed;
    public int damage;
    public string targetType;

    void Start()
    {
        Destroy(gameObject, 5f);
        _myRigidbody.linearVelocity = transform.up * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Character target = collision.gameObject.GetComponent<Character>();
        if (target && target.gameObject.CompareTag(targetType))  // unity auto converts to bool
        {
            target.health.Damage(damage);
            Destroy(gameObject);
        }
        if (_destroyEffect)     // for modularity and safety check
        {
            Instantiate(_destroyEffect, transform.position, transform.rotation);
        }
    }
}
