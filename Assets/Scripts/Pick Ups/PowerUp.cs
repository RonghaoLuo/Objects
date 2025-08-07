using UnityEngine;

public class PowerUp : MonoBehaviour, IPickable
{
    public virtual void PickUp()
    {
        ActivatePowerUp();

        Destroy(gameObject);
    }

    protected virtual void ActivatePowerUp()
    {

    }
}
