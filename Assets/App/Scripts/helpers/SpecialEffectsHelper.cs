using UnityEngine;

public class SpecialEffectsHelper : MonoBehaviour
{
    public static SpecialEffectsHelper Instance;

    public ParticleSystem playerExplosionEffect;
    public ParticleSystem enemyExplosionEffect;

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple instances of SpecialEffectsHelper!");
        }
        Instance = this;
    }

    public void PlayerExplosion(Vector3 position)
    {
        instantiate(playerExplosionEffect, position);
    }

    public void EnemyExplosion(Vector3 position)
    {
        instantiate(enemyExplosionEffect, position);
    }

    private ParticleSystem instantiate(ParticleSystem prefab, Vector3 position)
    {
        ParticleSystem newParticleSystem = Instantiate(prefab, position, Quaternion.identity);
        Destroy(newParticleSystem.gameObject, newParticleSystem.startLifetime);
        return newParticleSystem;
    }
}