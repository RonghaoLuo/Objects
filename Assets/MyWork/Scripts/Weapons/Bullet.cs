using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float speed;
    [SerializeField] private int penetration;
    [SerializeField] private int LayerIndex;

    [SerializeField] private Rigidbody2D _myRigidbody;
    [SerializeField] private GameObject hitEffect;

    private int remainingPenetration;

    public void Initialize(Transform weaponTip, int LayerIndex, float speed, int damage, int penetration)
    {
        this.LayerIndex = LayerIndex;
        this.speed = speed;
        this.damage = damage;
        this.penetration = penetration;
        remainingPenetration = penetration;

        gameObject.layer = LayerIndex;
    }

    void Start()
    {
        Destroy(gameObject, 2.5f);
        _myRigidbody.linearVelocity = transform.up * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Character target = collision.gameObject.GetComponentInParent<Character>();

        remainingPenetration -= target.GetPenetrationResistance();

        if (target)  // unity auto converts to bool
        {
            target.health.Damage(damage);
        }
        if (hitEffect)     // for modularity and safety check
        {
            Instantiate(hitEffect, transform.position, transform.rotation);
        }
        if (remainingPenetration <= 0)
        {
            Destroy(gameObject);
        }
    }
}
