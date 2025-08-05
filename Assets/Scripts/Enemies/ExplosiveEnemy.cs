using System;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveEnemy : CollisionEnemy
{
    [SerializeField] protected int _explosionDamage;
    [SerializeField] protected float _explosionRadius;
    [SerializeField] protected Transform _areaOfExplosion;

    public List<Character> charactersInRangeOfExplosion;
    public bool isExploded = false;

    protected override void Start()
    {
        base.Start();
        _areaOfExplosion.localScale = new Vector2 (_explosionRadius, _explosionRadius);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            health.Damage(health.GetHealth());
        }
    }

    protected override void Explode()
    {
        // safety checks
        if (isExploded)
        {
            return;
        }
        isExploded = true;

        // prevent modification of List during it's iteration; shallow copy
        List<Character> targets = new List<Character>(charactersInRangeOfExplosion);
        foreach (Character character in targets)
        {
            if (character == null)
            {
                continue;
            }
            character.health.Damage(_explosionDamage);
        }
        base.Explode();
    }
}
