using System;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveEnemy : Enemy
{
    [SerializeField] protected int _explosionDamage;
    [SerializeField] protected float _explosionRadius;
    [SerializeField] protected Transform _areaOfExplosion;

    public List<Character> charactersInRangeOfExplosion;

    protected override void Start()
    {
        ChangeSpriteColor(Color.red);
        player = FindAnyObjectByType<Player>();

        health.OnHealthZero +=
            (() =>
            {
                Invoke("Explode", 0.1f);
            });

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Explode();
        }
    }

    protected override void Explode()
    {
        foreach (Character character in charactersInRangeOfExplosion)
        {
            character.health.Damage(_explosionDamage);
        }
        base.Explode();
    }
}
