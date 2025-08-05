using UnityEngine;

public class RangeEnemy : Enemy
{
    [SerializeField] protected float distanceToAttack;

    protected override void FixedUpdate()
    {
        if (player != null)
        {
            Vector2 direction = player.transform.position - transform.position;
            if (Vector2.Distance(transform.position, player.transform.position) > distanceToAttack)
            {
                Move(direction.normalized, direction);
            }
            else
            {
                Attack();
            }
        }
    }
}
