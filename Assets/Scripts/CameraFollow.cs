using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _followSpeed;
    private Player _player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _player = FindAnyObjectByType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_player)
        {
            Vector3 destination = Vector3.Lerp(transform.position, _player.transform.position, Time.deltaTime * _followSpeed);
            destination.z = 0;
            transform.position = destination + _offset;
        }
    }
}
