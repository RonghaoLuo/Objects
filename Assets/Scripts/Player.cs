using UnityEngine;

public class Player : Character
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Move();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            Attack();
        }
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
}
