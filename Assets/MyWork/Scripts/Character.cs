using UnityEngine;

public class Character : MonoBehaviour
{
    public Health health;

    [SerializeField] protected int _maxHealth;
    [SerializeField] protected float _moveSpeed;
    [SerializeField] protected float _rotateSpeed;

    [SerializeField] protected Rigidbody2D myRigidbody;
    [SerializeField] protected SpriteRenderer sprite;
    [SerializeField] protected GameObject destroyEffect;
    [SerializeField] protected AudioSource attackAudio;

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
        
    }

    protected virtual void Explode()
    {
        if (destroyEffect)
        {
            Instantiate(destroyEffect,transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }

    protected void ChangeSpriteColor(Color newColor)
    {
        sprite.color = newColor;
    }

    protected void ChangeSpriteColor(float red, float green, float blue)
    {
        Color newColor = new Color(red, green, blue);
        ChangeSpriteColor(newColor);
    }

    public virtual void Move(Vector2 direction)
    {
        myRigidbody.AddForce(direction * _moveSpeed);
    }

    public virtual void Move(Vector2 directionToMove, Vector2 directionToLook)
    {
        Move(directionToMove);
        LookAt(directionToLook);
    }

    public virtual void LookAt(Vector2 direction)
    {
        transform.up = Vector2.Lerp(transform.up, direction, Time.fixedDeltaTime * _rotateSpeed);
    }
}
