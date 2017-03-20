using UnityEngine;
using UnityEngine.Networking;

public class Evade : NetworkBehaviour
{
    public float amplitude;
    public Vector2 speedRange;

    private Rigidbody _rigidbody;

    [SyncVar] private float _speed;
    [SyncVar] private float _time;

    void Start()
    {
        if (!isServer) return;

        _speed = Random.Range(speedRange.x, speedRange.y);
        _time = Random.Range(0.0f, 1.0f);
    }

    void Update()
    {
        _time += Time.deltaTime * _speed;
    }

    void FixedUpdate()
    {
        if (_rigidbody == null)
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        Vector3 newPosition = _rigidbody.position;
        newPosition.x = Mathf.Cos(_time * Mathf.PI * 2) * amplitude;
        _rigidbody.position = newPosition;
    }
}