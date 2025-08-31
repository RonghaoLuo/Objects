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
        GameManager.Instance.OnPlayerSpawn += SetPlayerReference;
        GameManager.Instance.OnGameEnd += RemovePlayerReference;
        health.OnHealthZero += DoOnHealthZero;
        health.OnHealthZero += TrySpawnItem;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnPlayerSpawn -= SetPlayerReference;
        GameManager.Instance.OnGameEnd -= RemovePlayerReference;
        health.OnHealthZero -= DoOnHealthZero;
        health.OnHealthZero -= TrySpawnItem;
    }

    protected override void Start()
    {
        allSpawnedEnemies.Add(this);
        ChangeSpriteColor(Color.orange);
        OnAllSpawnedEnemiesChange?.Invoke(allSpawnedEnemies.Count);
        
        if (player == null)
        {
            player = GameManager.Instance.GetPlayerReference();
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

    private void TrySpawnItem()
    {
        ItemSpawnerManager.Instance.TrySpawnItem(transform.position, transform.rotation);
    }
}
