using UnityEngine;

public class Character : MonoBehaviour
{
    public Health health;

    [SerializeField] protected int _maxHealth;
    [SerializeField] protected float _moveSpeed;

    [SerializeField] protected Rigidbody2D _myRigidbody;
    [SerializeField] protected SpriteRenderer _sprite;

    // reserved time to do stuff before the game starts
    protected virtual void Awake()
    {
        health = new Health(_maxHealth);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        ChangeSpriteColor(Random.value, Random.value, Random.value);
    }

    protected virtual void Update()
    {

    }

    public virtual void Attack()
    {
        Debug.Log("Character Attack");
    }

    protected virtual void Explode()
    {

    }

    protected void ChangeSpriteColor(Color newColor)
    {
        _sprite.color = newColor;
    }

    protected void ChangeSpriteColor(float red, float green, float blue)
    {
        Color newColor = new Color(red, green, blue);
        ChangeSpriteColor(newColor);
    }

    public virtual void Move(Vector2 direction)
    {
        _myRigidbody.AddForce(direction * _moveSpeed);
    }

    public virtual void Move(Vector2 directionToMove, Vector2 directionToLook)
    {
        Move(directionToMove);
        transform.up = directionToLook;
    }
}
