using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float speed;
    [SerializeField] private int LayerIndex;

    [SerializeField] private Rigidbody2D _myRigidbody;
    [SerializeField] private GameObject _destroyEffect;
    public void Initialize(Transform weaponTip, int LayerIndex, float speed, int damage)
    {
        this.LayerIndex = LayerIndex;
        this.speed = speed;
        this.damage = damage;

        gameObject.layer = LayerIndex;
    }

    void Start()
    {
        Destroy(gameObject, 2.5f);
        _myRigidbody.linearVelocity = transform.up * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Character target = collision.gameObject.GetComponent<Character>();
        if (target)  // unity auto converts to bool
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
