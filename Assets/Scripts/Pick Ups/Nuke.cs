using UnityEngine;
using System.Collections.Generic;

public class Nuke : PowerUp
{
    private Player _player;

    private void Awake()
    {
        _player = FindAnyObjectByType<Player>();
    }

    public override void PickUp()
    {
        if (_player.numOfNukes < 3)
        {
            _player.numOfNukes++;
            _player.OnNukeChange?.Invoke();
            Destroy(gameObject);
        }
    }
}
