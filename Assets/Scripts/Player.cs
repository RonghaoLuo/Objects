using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Player : Character
{
    public Action OnPlayerDie, OnNukeChange;
    public Action<float> OnStartFullAuto;
    public WeaponData currentWeapon;
    public int numOfNukes = 0;

    private Vector2 _moveDirection;
    private Vector2 _lookDirection;
    private Vector2 _worldPositionOfMouse;
    //private PlayerInventory _myInventory;
    private bool isOnFullAuto = false;
    private float _fullAutoDuration;
    private float _nextAttackTime = 0f;

    [SerializeField] private Transform _weaponTip;
    [SerializeField] private float _currentAttackCooldown;
    [SerializeField] private float _semiAutoAttackCooldown;
    [SerializeField] private float _fullAutoAttackCooldown;

    protected override void Awake()
    {
        base.Awake();
        OnStartFullAuto += StartFullAuto;
    }

    protected override void Start()
    {
        //currentWeapon = new WeaponData(bulletPrefab, shootOrigin);
        ChangeSpriteColor(Color.blue);
        _currentAttackCooldown = _semiAutoAttackCooldown;
    }

    protected override void Update()
    {
        base.Update();

        _moveDirection.x = Input.GetAxisRaw("Horizontal");
        _moveDirection.y = Input.GetAxisRaw("Vertical");
        _worldPositionOfMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _lookDirection = _worldPositionOfMouse - myRigidbody.position;

        if (!isOnFullAuto && Input.GetMouseButtonDown(0))
        {
            TryAttack();
        }
        else if (isOnFullAuto && Input.GetMouseButton(0))
        {
            TryAttack();
        }
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    health.Damage(10);
        //}
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
    public void TryAttack()
    {
        if (Time.time > _nextAttackTime)
        {
            Attack();
            _nextAttackTime = Time.time + _currentAttackCooldown;
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

    private void StartFullAuto(float duration)
    {
        // enable the children object for UI
        isOnFullAuto = true;
        _currentAttackCooldown = _fullAutoAttackCooldown;
        // need proper timer for stopfullauto
    }

    private void StopFullAuto()
    {
        isOnFullAuto = false;
        _currentAttackCooldown = _semiAutoAttackCooldown;
        // disable the children object for UI
    }
}
