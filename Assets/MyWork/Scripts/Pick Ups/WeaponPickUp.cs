using UnityEngine;

public class WeaponPickUp : MonoBehaviour, IPickable
{
    [SerializeField] private int weaponIndex;
    protected Player _player;

    protected virtual void Start()
    {
        ItemSpawnerManager.Instance.allManagerSpawnedItems.Add(gameObject);

        GameManager.Instance.OnPlayerSpawn += SetPlayer;
        GameManager.Instance.OnGameEnd += RemovePlayer;
        if (_player == null)
        {
            _player = GameManager.Instance.GetPlayerReference();
        }
    }

    protected virtual void OnDestroy()
    {
        ItemSpawnerManager.Instance.allManagerSpawnedItems.Remove(gameObject);

        GameManager.Instance.OnPlayerSpawn -= SetPlayer;
        GameManager.Instance.OnGameEnd -= RemovePlayer;
    }

    protected void SetPlayer(Player player)
    {
        _player = player;
    }

    protected void RemovePlayer()
    {
        _player = null;
    }

    public virtual void PickUp()
    {
        _player.SetActiveWeaponWithIndex(weaponIndex, true);

        Destroy(gameObject);
    }
}
