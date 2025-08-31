using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Player : Character
{
    public Action OnNukeChange;
    public Action<float> OnStartFullAuto;
    public WeaponData currentWeapon;
    public int numOfNukes = 0;

    private Vector2 _moveDirection;
    private Vector2 _lookDirection;
    private Vector2 _worldPositionOfMouse;
    //private PlayerInventory _myInventory;
    private bool isOnFullAuto = false;
    private float _nextAttackTime = 0f;
    private Coroutine activeFullAutoCoroutine;

    [SerializeField] private Transform _weaponTip;
    [SerializeField] private UIFullAuto _fullAutoUI;

    [SerializeField] private float _currentAttackCooldown;
    [SerializeField] private float _semiAutoAttackCooldown;
    [SerializeField] private float _fullAutoAttackCooldown;

    protected override void Awake()
    {
        base.Awake();
        OnStartFullAuto += StartFullAuto;
        health.OnHealthZero += Explode;
        health.OnHealthZero += GameManager.Instance.EndGame;
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

        ChangeSpriteColor(Color.Lerp(Color.red, Color.green, health.GetHealthInFraction()));
    }

    private void FixedUpdate()
    {
        Move(_moveDirection.normalized, _lookDirection);
    }

    protected override void Explode()
    {
        OnStartFullAuto -= StartFullAuto;
        health.OnHealthZero -= Explode;
        health.OnHealthZero -= GameManager.Instance.EndGame;

        Debug.Log("Game Over");
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
        if (CharacterAudio != null)
        {
            CharacterAudio.volume = currentWeapon.fireAudioVolume;
            CharacterAudio.pitch = currentWeapon.fireAudioPitch;
            CharacterAudio.PlayOneShot(currentWeapon.fireAudio);
        }
        base.Attack();
        if (currentWeapon != null)
        {
            currentWeapon.ShootWeapon(_weaponTip, 8);
        }
    }

    private void UseNuke()
    {
        if (numOfNukes < 1) return;

        numOfNukes--;
        OnNukeChange?.Invoke();
        
        EnemyManager.Instance.KillAllEnemy();
    }

    private void StartFullAuto(float duration)
    {
        if (activeFullAutoCoroutine != null)
        {
            StopCoroutine(activeFullAutoCoroutine);
        }

        activeFullAutoCoroutine = StartCoroutine(StartFullAutoCoroutine(duration));
    }

    private IEnumerator StartFullAutoCoroutine(float duration)
    {
        isOnFullAuto = true;
        _currentAttackCooldown = _fullAutoAttackCooldown;
        _fullAutoUI.StartCountdown(duration); // enable the children object for UI

        yield return new WaitForSeconds(duration);

        isOnFullAuto = false;
        _currentAttackCooldown = _semiAutoAttackCooldown;
    }
}
