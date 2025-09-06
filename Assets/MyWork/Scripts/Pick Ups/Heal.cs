using UnityEngine;

public class Heal : PowerUp
{
    [SerializeField] private int healAmount;

    protected override void ActivatePowerUp()
    {
        base.ActivatePowerUp();
        _player.health.Heal(healAmount);
    }
}
