using UnityEngine;

public class MeleeEnemy : Enemy
{
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Attack();
        }
    }

    public override void Attack()
    {
        Explode();
    }
}
