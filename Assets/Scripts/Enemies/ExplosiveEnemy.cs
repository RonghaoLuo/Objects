using System;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveEnemy : Enemy
{
    [SerializeField] protected float _explosionRadius;
    [SerializeField] protected Transform _areaOfExplosion;

    public List<Character> charactersInRangeOfExplosion;

    protected override void Start()
    {
        base.Start();
        _areaOfExplosion.localScale = new Vector2 (_explosionRadius, _explosionRadius);
    }

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

    protected override void Update()
    {
        base .Update();
    }
}
