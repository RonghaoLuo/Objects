using UnityEngine;

public class PowerUp : MonoBehaviour, IPickable
{
    public void PickUp()
    {
        ActivatePowerUp();

        Destroy(gameObject);
    }

    protected virtual void ActivatePowerUp()
    {

    }
}
