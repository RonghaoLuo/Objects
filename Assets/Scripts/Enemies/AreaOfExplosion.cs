using UnityEngine;

public class AreaOfExplosion : MonoBehaviour
{
    [SerializeField] private ExplosiveEnemy _myEnemy;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // check if colliding object is a character
        // add character
        _myEnemy.charactersInRangeOfExplosion.Add
            (collision.gameObject.GetComponent<ExplosiveEnemy>());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }
}
