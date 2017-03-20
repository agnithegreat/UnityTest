using UnityEngine;
using UnityEngine.Networking;

public class Move : NetworkBehaviour
{
    [SyncVar]
    public float speed;

    [SyncVar]
    public Vector3 direction;

    private Rigidbody _rigidbody;

    void FixedUpdate()
    {
        if (_rigidbody == null)
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        _rigidbody.velocity = direction * speed;
    }
}