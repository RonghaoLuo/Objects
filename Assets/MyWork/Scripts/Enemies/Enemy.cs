using System;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] protected int score;
    [SerializeField] protected float _attackCooldown = 1f;
    [SerializeField] protected int _attackDamage;
    [SerializeField] protected bool chasePlayer;
    [SerializeField] protected float nukeResistanceFraction;

    protected float _nextAttackTime = 0f;

    protected Player player;

    public static List<Enemy> allSpawnedEnemies = new List<Enemy>();
    public static List<StationEnemy> allSpawnedStations = new List<StationEnemy>();

    public static Action<int> OnAllSpawnedEnemiesChange;
    public static Action<int> OnAllSpawnedStationsChange;

    protected override void OnDestroy()
    {
        GameManager.Instance.OnPlayerSpawn -= SetPlayerReference;
        GameManager.Instance.OnGameEnd -= RemovePlayerReference;
        health.OnHealthZero -= DoOnHealthZero;
        health.OnHealthZero -= TrySpawnDrops;

        base.OnDestroy();
    }

    protected override void Start()
    {
        GameManager.Instance.OnPlayerSpawn += SetPlayerReference;
        GameManager.Instance.OnGameEnd += RemovePlayerReference;
        health.OnHealthZero += DoOnHealthZero;
        health.OnHealthZero += TrySpawnDrops;

        if (this is StationEnemy station)
        {
            allSpawnedStations.Add(station);
            OnAllSpawnedStationsChange?.Invoke(allSpawnedStations.Count);
        }
        else
        {
            allSpawnedEnemies.Add(this);
            OnAllSpawnedEnemiesChange?.Invoke(allSpawnedEnemies.Count);
        }

        ChangeSpriteColor(Color.orange);
        
        if (player == null)
        {
            player = GameManager.Instance.GetPlayerReference();
        }
    }

    protected virtual void FixedUpdate()
    {
        if (!player || !chasePlayer)
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
        if (this is StationEnemy station)
        {
            allSpawnedStations.Remove(station);
            OnAllSpawnedStationsChange?.Invoke(allSpawnedStations.Count);
        }
        else
        {
            allSpawnedEnemies.Remove(this);
            OnAllSpawnedEnemiesChange?.Invoke(allSpawnedEnemies.Count);
        }
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

    protected virtual void TrySpawnDrops()
    {
        ItemSpawnerManager.Instance.TrySpawnItem(transform.position);
    }

    public float GetNukeResistanceFraction()
    {
        return nukeResistanceFraction;
    }
}
