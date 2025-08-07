using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Player : Character
{
    public Action OnPlayerDie, OnNukeChange;
    public WeaponData currentWeapon;
    public int numOfNukes = 0;

    private Vector2 _moveDirection;
    private Vector2 _lookDirection;
    private Vector2 _worldPositionOfMouse;
    //private PlayerInventory _myInventory;

    [SerializeField] private Transform _weaponTip;

    protected override void Start()
    {
        //currentWeapon = new WeaponData(bulletPrefab, shootOrigin);
        ChangeSpriteColor(Color.blue);
    }

    protected override void Update()
    {
        base.Update();

        _moveDirection.x = Input.GetAxisRaw("Horizontal");
        _moveDirection.y = Input.GetAxisRaw("Vertical");
        _worldPositionOfMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _lookDirection = _worldPositionOfMouse - myRigidbody.position;

        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            health.Damage(10);
        }
        if (Input.GetMouseButtonDown(1))
        {
            UseNuke();
        }

        ChangeSpriteColor(Color.Lerp(Color.red, Color.green, (float)health.GetHealth() / _maxHealth));
    }

    private void FixedUpdate()
    {
        Move(_moveDirection.normalized, _lookDirection);
    }

    protected override void Explode()
    {
        Debug.Log("Game Over");
        OnPlayerDie.Invoke();
        base.Explode();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IPickable pickableFeature = collision.gameObject.GetComponent<IPickable>();
        if (pickableFeature != null)
        {
            pickableFeature.PickUp();
        }
    }

    public override void Attack()
    {
        CharacterAudio.volume = currentWeapon.fireAudioVolume;
        CharacterAudio.pitch = currentWeapon.fireAudioPitch;
        CharacterAudio.PlayOneShot(currentWeapon.fireAudio);
        base.Attack();
        if (currentWeapon == null)
        {
            return;
        }
        currentWeapon.ShootWeapon(_weaponTip);
    }

    private void UseNuke()
    {
        if (numOfNukes < 1) return;

        numOfNukes--;
        OnNukeChange?.Invoke();
        List<Enemy> targets = new List<Enemy>(Enemy.allSpawnedEnemies);
        foreach (Enemy target in targets)
        {
            if (target == null) continue;
            target.health.Kill();
        }
    }
}
