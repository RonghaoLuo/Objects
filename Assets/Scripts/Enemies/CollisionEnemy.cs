using UnityEngine;

public class CollisionEnemy : Enemy
{
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
        target.health.Damage(_attackDamage);
    }
}
