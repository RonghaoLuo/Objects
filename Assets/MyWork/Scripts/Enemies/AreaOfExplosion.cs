using UnityEngine;

public class AreaOfExplosion : MonoBehaviour
{
    [SerializeField] private ExplosiveEnemy _myEnemy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Character collidedChar = collision.GetComponentInParent<Character>();
        if (collidedChar == null)
        {
            return;
        }

        _myEnemy.charactersInRangeOfExplosion.Add(collidedChar);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Character collidedChar = collision.GetComponentInParent<Character>();
        if (collidedChar == null)
        {
            return;
        }

        _myEnemy.charactersInRangeOfExplosion.Remove(collidedChar);
    }
}
