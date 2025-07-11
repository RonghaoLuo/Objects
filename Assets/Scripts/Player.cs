using UnityEngine;

public class Player : Character
{
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform shootOrigin;
    [SerializeField] private float _bulletSpeed;

    private Vector2 _moveDirection;
    private Vector2 _lookDirection;
    
    private Vector2 _mousePosition;
    private Vector2 _worldPositionOfMouse;
    private WeaponData currentWeapon;

    private void Update()
    {
        _moveDirection.x = Input.GetAxisRaw("Horizontal");
        _moveDirection.y = Input.GetAxisRaw("Vertical");

        _mousePosition = Input.mousePosition;
        _worldPositionOfMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        _lookDirection = _worldPositionOfMouse - myRigidbody.position;

        Move(_moveDirection * Time.fixedDeltaTime, _lookDirection);

        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    protected override void Start()
    {
        currentWeapon = new WeaponData(bulletPrefab, shootOrigin);
        ChangeSpriteColor(Color.blue);
    }

    protected override void Explode()
    {
        base.Explode();
        Debug.Log("Game Over");
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
        base.Attack();
        currentWeapon.ShootWeapon();
    }
}
