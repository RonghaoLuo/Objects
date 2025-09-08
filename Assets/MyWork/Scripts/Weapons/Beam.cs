using UnityEngine;

public class Beam : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float residenceTime;

    [SerializeField] private Rigidbody2D _myRigidbody;
    [SerializeField] private GameObject hitEffect;

    private int remainingPenetration;

    public void Initialize(Transform weaponTip, int damage)
    {
        this.damage = damage;
    }

    void Start()
    {
        Destroy(gameObject, residenceTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Character target = collision.gameObject.GetComponentInParent<Character>();

        if (target)  // unity auto converts to bool
        {
            target.health.Damage(damage);
        }
        if (hitEffect)     // for modularity and safety check
        {
            Instantiate(hitEffect, transform.position, transform.rotation);
        }
    }
}
