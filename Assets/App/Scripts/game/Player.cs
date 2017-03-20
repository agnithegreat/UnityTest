using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{
    public float speed;
    public float tilt;
    public Boundary boundary;

    private Rigidbody _rigidbody;
    private Weapon _weapon;
    private Health _health;
    private Move _move;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _weapon = GetComponent<Weapon>();
        _health = GetComponent<Health>();
        _move = GetComponent<Move>();
    }

    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        _move.direction = new Vector3(
            inputX,
            inputY,
            0.0f
        );

        bool shoot = Input.GetKey(KeyCode.Space);
        if (shoot)
        {
            if (_weapon != null && _weapon.CanAttack)
            {
                _weapon.Attack(false);
            }
        }
    }

    void FixedUpdate()
    {
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, boundary.xMin, boundary.xMax),
            Mathf.Clamp(transform.position.y, boundary.yMin, boundary.yMax),
            0.0f
        );

        transform.rotation = Quaternion.Euler(
            -90.0f,
            0.0f,
            _rigidbody.velocity.x * -tilt
        );
    }

    void OnTriggerEnter(Collider otherCollider)
    {
        Health enemyHealth = otherCollider.gameObject.GetComponent<Health>();
        if (enemyHealth != null && _health != null && enemyHealth.isEnemy != _health.isEnemy)
        {
            enemyHealth.Damage(1);
            _health.Damage(1);
        }
    }
}