using UnityEngine;

public class Enemy : Character
{
    [SerializeField] protected float distanceToAttack;

    protected Player _player;

    public override void Attack()
    {
        Debug.Log("Pow Pow");
    }

    protected override void Start()
    {
        ChangeSpriteColor(Color.red);

        _player = FindAnyObjectByType<Player>();
    }
}
