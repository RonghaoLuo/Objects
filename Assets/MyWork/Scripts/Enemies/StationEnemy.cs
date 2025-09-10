using UnityEngine;

public class StationEnemy : Enemy
{
    protected override void TrySpawnDrops()
    {
        ItemSpawnerManager.Instance.TrySpawnWeaponDrop(transform.position);
    }
}
