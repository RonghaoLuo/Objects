using UnityEngine;

public class Player : Character
{
    private Vector2 _moveDirection;
    
    [SerializeField] private Vector2 _mousePosition;
    [SerializeField] private Vector3 _worldPositionOfMouse;

    private void Update()
    {
        _moveDirection.x = Input.GetAxisRaw("Horizontal");
        _moveDirection.y = Input.GetAxisRaw("Vertical");

        _mousePosition = Input.mousePosition;

        _worldPositionOfMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        _worldPositionOfMouse.z = 0;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    private void FixedUpdate()
    {
        Move(_moveDirection);
    }

    protected override void Start()
    {
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
}
