using UnityEngine;

public class RangeEnemy : Enemy
{
    [SerializeField] protected float distanceToAttack;
    [SerializeField] protected WeaponData currentWeapon;
    [SerializeField] protected Transform weaponTip;

    protected override void FixedUpdate()
    {
        if (player != null)
        {
            Vector2 direction = player.transform.position - transform.position;
            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
            if (distanceToPlayer > distanceToAttack)
            {
                Move(direction.normalized);
            }
        }
    }

    protected override void Update()
    {
        base.Update();
        if (player != null)
        {
            Vector2 direction = player.transform.position - transform.position;
            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
            
            LookAt(direction.normalized);

            if (distanceToPlayer <= distanceToAttack)
            {
                TryAttack(player);
            }
        }
    }

    public virtual void TryAttack(Character target)
    {
        if (Time.time > _nextAttackTime)
        {
            Attack(target);
            _nextAttackTime = Time.time + _attackCooldown;
        }
    }

    protected virtual void Attack(Character target)
    {
        if (CharacterAudio != null)
        {
            CharacterAudio.volume = currentWeapon.fireAudioVolume;
            CharacterAudio.pitch = currentWeapon.fireAudioPitch;
            CharacterAudio.PlayOneShot(currentWeapon.fireAudio);
        }
        
        if (currentWeapon != null)
        {
            currentWeapon.ShootWeapon(weaponTip, 7);
        }
    }
}