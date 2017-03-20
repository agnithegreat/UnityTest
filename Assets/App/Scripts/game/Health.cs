using UnityEngine;
using UnityEngine.Networking;

public class Health : NetworkBehaviour
{
    [SyncVar] public int hp;
    [SyncVar] public bool isEnemy = true;

    public void Damage(int damageCount)
    {
        hp -= damageCount;

        if (!alive)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider otherCollider)
    {
        Shot shot = otherCollider.gameObject.GetComponent<Shot>();
        if (shot != null)
        {
            if (shot.isEnemyShot != isEnemy)
            {
                Damage(shot.damage);

                Destroy(shot.gameObject);
            }
        }
    }

    public bool alive
    {
        get { return hp > 0; }
    }

    void OnDestroy()
    {
        if (!alive)
        {
            if (isEnemy)
            {
                SpecialEffectsHelper.Instance.EnemyExplosion(transform.position);
                SoundEffectsHelper.Instance.MakeEnemyExplosionSound();
            } else {
                SpecialEffectsHelper.Instance.PlayerExplosion(transform.position);
                SoundEffectsHelper.Instance.MakePlayerExplosionSound();
            }
        }
    }
}