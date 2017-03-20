using UnityEngine;
using UnityEngine.Networking;

public class RandomRotator : NetworkBehaviour
{
	public float tumble;

    [SyncVar(hook="UpdateVelocity")]
    private Vector3 _angularVelocity;

    private Rigidbody _rigidbody;

    void Start()
    {
        if (!isServer) return;

        _angularVelocity = Random.insideUnitSphere * tumble;
        UpdateVelocity(_angularVelocity);
    }

    private void UpdateVelocity(Vector3 velocity)
    {
        if (_rigidbody == null)
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
        _rigidbody.angularVelocity = velocity;
    }
}