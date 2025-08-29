using System;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] protected int score;
    [SerializeField] protected float _attackCooldown = 1f;
    [SerializeField] protected int _attackDamage;
    protected float _nextAttackTime = 0f;

    protected Player player;

    public static List<Enemy> allSpawnedEnemies = new List<Enemy>();
    public static Action<int> OnAllSpawnedEnemiesChange;

    protected override void Awake()
    {
        base.Awake();
        GameManager.Singleton.OnPlayerSpawn += SetPlayerReference;
        GameManager.Singleton.OnGameEnd += RemovePlayerReference;
        health.OnHealthZero += DoOnHealthZero;
    }

    protected override void Start()
    {
        allSpawnedEnemies.Add(this);
        ChangeSpriteColor(Color.orange);
        OnAllSpawnedEnemiesChange?.Invoke(allSpawnedEnemies.Count);
        
        if (player == null)
        {
            player = GameManager.Singleton.GetPlayerReference();
        }
    }

    protected virtual void FixedUpdate()
    {
        if (!player)
        {
            return;
        }

        Vector2 direction = player.transform.position - transform.position;
        Move(direction.normalized, direction);
    }

    public override void Attack()
    {
        //Debug.Log("Enemy Attack");
    }

    protected override void Explode()
    {
        health.OnHealthZero -= DoOnHealthZero;
        GameManager.Singleton.OnPlayerSpawn -= SetPlayerReference;
        GameManager.Singleton.OnGameEnd -= RemovePlayerReference;
        ScoreManager.Instance.AddScore(score);
        base.Explode();
    }

    protected virtual void DoOnHealthZero()
    {
        allSpawnedEnemies.Remove(this);
        OnAllSpawnedEnemiesChange?.Invoke(allSpawnedEnemies.Count);
        Explode();
    }

    protected void SetPlayerReference(Player player)
    {
        this.player = player;
    }

    protected void RemovePlayerReference()
    {
        player = null;
    }
}
