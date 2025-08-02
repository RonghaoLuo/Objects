using UnityEngine;

public class MeleeEnemy : Enemy
{
    [SerializeField] private float _attackCooldown = 1f;
    [SerializeField] private int _attactDamage;

    private float _nextAttackTime = 0f;

    protected override void FixedUpdate()
    {
        // movements
        if (!player)
        {
            return;
        }

        Vector2 direction = player.transform.position - transform.position;
        Move(direction.normalized, direction);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            TryAttack(player);
        }
    }

    public void TryAttack(Character target)
    {
        if (Time.time > _nextAttackTime)
        {
            Attack(target);
            _nextAttackTime = Time.time + _attackCooldown;
        }
    }

    private void Attack(Character target)
    {
        target.health.Damage(_attactDamage);
    }
}
