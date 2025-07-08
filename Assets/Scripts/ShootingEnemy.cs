using UnityEngine;

public class ShootingEnemy : Enemy, IShootable
{
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private float _projectileSpeed = 1f;
    [SerializeField] private float _shootInverval = 1;

    private float _timer = 0f;


    public override void Attack()
    {
        Debug.Log("Shoot Shoot");
    }

    public void ShootTowards(GameObject target)
    {
        GameObject projectile = Instantiate(
            _projectilePrefab, transform.position, Quaternion.identity);

        if (projectile == null || target == null) return;

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector3 dir = (target.transform.position - projectile.transform.position).normalized;
            rb.linearVelocity = dir * _projectileSpeed;
        }
    }

    protected override void Start()
    {
        base.Start();

        ShootTowards(_player.gameObject);
    }

    protected override void Update()
    {
        base.Update();

        _timer += Time.deltaTime;
        if (_timer >= _shootInverval)
        {
            _timer = 0;
            ShootTowards(_player.gameObject);
        }
    }
}
