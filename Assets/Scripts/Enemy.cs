using UnityEngine;

public class Enemy : Character
{
    [SerializeField] protected float distanceToAttack;

    private Player player;

    protected override void Start()
    {
        ChangeSpriteColor(Color.red);
        player = FindAnyObjectByType<Player>();
    }

    private void FixedUpdate()
    {
        if (player != null)
        {
            Vector2 direction = player.transform.position - transform.position;
            if (Vector2.Distance(transform.position, player.transform.position) > distanceToAttack)
            {
                Move(direction, direction);
            }
            else
            {
                Attack();
            }
        }
    }

    public override void Attack()
    {
        //Debug.Log("Enemy Attack");
    }
}
