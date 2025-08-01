using System;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] protected int score;
    [SerializeField] protected float distanceToAttack;

    protected Player player;

    public static List<Enemy> allSpawnedEnemies = new List<Enemy>();
    public static Action<int> OnAllSpawnedEnemiesChange;

    protected override void Start()
    {
        allSpawnedEnemies.Add(this);
        ChangeSpriteColor(Color.red);
        player = FindAnyObjectByType<Player>();
        OnAllSpawnedEnemiesChange?.Invoke(allSpawnedEnemies.Count);

        health.OnHealthZero +=
            (() =>
            {
                allSpawnedEnemies.Remove(this);
                OnAllSpawnedEnemiesChange.Invoke(allSpawnedEnemies.Count);
                Explode();
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
        ScoreManager.Instance.AddScore(score);
        base.Explode();
    }
}
