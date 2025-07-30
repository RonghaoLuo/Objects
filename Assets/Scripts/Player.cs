using UnityEngine;
using System;

public class Player : Character
{
    public Action OnPlayerDie;
    public WeaponData currentWeapon;

    private Vector2 _moveDirection;
    private Vector2 _lookDirection;
    private Vector2 _worldPositionOfMouse;

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

    public void PickUp()
    {
        Destroy(gameObject);
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
}
