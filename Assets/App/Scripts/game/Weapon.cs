using UnityEngine;
using UnityEngine.Networking;

public class Weapon : NetworkBehaviour
{
    public GameObject shotSpawn;

    public float shootingRate = 0.25f;
    public float shootingSpeed = 0.25f;

    private float _shootCooldown;

    private Pool _shotsPool;

    void Start()
    {
        _shootCooldown = 0.0f;
        _shotsPool = GetComponent<Pool>();
    }

    void Update()
    {
        if (_shootCooldown > 0)
        {
            _shootCooldown -= Time.deltaTime;
        }
    }

    public void Attack(bool isEnemy)
    {
        if (CanAttack)
        {
            SoundEffectsHelper.Instance.MakePlayerShotSound();

            _shootCooldown = shootingRate;

            CmdSpawnShot(isEnemy);
        }
    }

    [Command]
    void CmdSpawnShot(bool isEnemy)
    {
        GameObject instance = _shotsPool.Create();
        instance.transform.position = shotSpawn.transform.position;
        NetworkServer.Spawn(instance);

        Shot shot = instance.GetComponent<Shot>();
        if (shot != null)
        {
            shot.isEnemyShot = isEnemy;
        }

        Move move = instance.GetComponent<Move>();
        if (move != null)
        {
            move.speed = shootingSpeed;
            move.direction = shotSpawn.transform.forward;
        }
    }

    public bool CanAttack
    {
        get { return _shootCooldown <= 0.0f; }
    }
}