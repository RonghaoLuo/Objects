using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PowerUp : MonoBehaviour, IPickable
{
    protected Player _player;

    private void Start()
    {
        GameManager.Instance.OnPlayerSpawn += SetPlayer;
        GameManager.Instance.OnGameEnd += RemovePlayer;
        if (_player == null)
        {
            _player = GameManager.Instance.GetPlayerReference();
        }
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnPlayerSpawn -= SetPlayer;
        GameManager.Instance.OnGameEnd -= RemovePlayer;
    }

    void SetPlayer(Player player)
    {
        _player = player;
    }

    void RemovePlayer()
    {
        _player = null;
    }

    public virtual void PickUp()
    {
        ActivatePowerUp();

        Destroy(gameObject);
    }

    protected virtual void ActivatePowerUp()
    {

    }
}
