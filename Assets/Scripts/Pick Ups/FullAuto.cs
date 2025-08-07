using UnityEngine;

public class FullAuto : PowerUp
{
    private Player _player;
    [SerializeField] private float _fullAutoDuration;

    private void Awake()
    {
        _player = FindAnyObjectByType<Player>();
    }

    public override void PickUp()
    {
        base.PickUp();
    }

    protected override void ActivatePowerUp()
    {
        base.ActivatePowerUp();
        _player.OnStartFullAuto?.Invoke(_fullAutoDuration);
    }
}
