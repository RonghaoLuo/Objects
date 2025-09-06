using UnityEngine;

public class FullAuto : PowerUp
{
    [SerializeField] private float _fullAutoDuration;

    protected override void ActivatePowerUp()
    {
        base.ActivatePowerUp();
        _player.OnStartFullAuto?.Invoke(_fullAutoDuration);
    }
}
