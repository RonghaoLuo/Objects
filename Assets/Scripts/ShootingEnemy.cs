using UnityEngine;

public class ShootingEnemy : Enemy, IShootable
{
    [SerializeField] private GameObject _projectilePrefab;

    public override void Attack()
    {
        Debug.Log("Shoot Shoot");
    }

    public void ShootTowards(GameObject target)
    {
        GameObject projectile = Instantiate(_projectilePrefab, transform.position, Quaternion.identity);
    }

    protected override void Start()
    {
        base.Start();
        ShootTowards(this.gameObject);
    }
}
