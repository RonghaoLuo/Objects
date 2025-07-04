using UnityEngine;

public class Character : MonoBehaviour
{
    public Health health;

    [SerializeField] protected float moveSpeed;
    [SerializeField] protected Rigidbody2D myRigidbody;
    [SerializeField] protected SpriteRenderer sprite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        ChangeSpriteColor(Color.white);
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

    protected virtual void ChangeSpriteColor(Color newColor)
    {
        sprite.color = newColor;
    }
}
