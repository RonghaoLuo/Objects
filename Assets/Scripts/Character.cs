using UnityEngine;

public class Character : MonoBehaviour
{
    public Health health;

    [SerializeField] protected int maxHealth;
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected Rigidbody2D myRigidbody;
    [SerializeField] protected SpriteRenderer sprite;

    // reserved time to do stuff before the game starts
    private void Awake()
    {
        health = new Health(maxHealth);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        ChangeSpriteColor(Random.value, Random.value, Random.value);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void Attack()
    {
        Debug.Log("Pew Pew");
    }

    public virtual void Move(Vector2 direction)
    {
        myRigidbody.AddForce(direction * moveSpeed);
    }

    protected virtual void Explode()
    {

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

    public virtual void Move(Vector2 directionToMove, Vector2 directionToLook)
    {
        Move(directionToMove);
        transform.up = directionToLook;
    }
}
