using System;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] protected int score;
    [SerializeField] protected float distanceToAttack;

    protected Player player;

    protected override void Start()
    {
        ChangeSpriteColor(Color.red);
        player = FindAnyObjectByType<Player>();

        health.OnHealthZero +=
            (() =>
            {
                Invoke("Explode", 0.1f);
            });
    }

    protected virtual void FixedUpdate()
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

    public override void Attack()
    {
        //Debug.Log("Enemy Attack");
    }

    protected override void Explode()
    {
        base.Explode();
        ScoreManager.Instance.AddScore(score);
        Destroy(gameObject);
    }
}
