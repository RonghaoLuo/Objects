using UnityEngine;

public class PlayerBounds : MonoBehaviour
{
    private RectTransform minBounds, maxBounds;

    private void Awake()
    {
        minBounds = GameManager.Instance.playerMinBounds;
        maxBounds = GameManager.Instance.playerMaxBounds;
    }

    void LateUpdate()
    {
        Vector3 playerPosition = transform.position;

        playerPosition.x = Mathf.Clamp(playerPosition.x, minBounds.position.x, maxBounds.position.x);
        playerPosition.y = Mathf.Clamp(playerPosition.y, minBounds.position.y, maxBounds.position.y);
        transform.position = playerPosition;
    }
}
