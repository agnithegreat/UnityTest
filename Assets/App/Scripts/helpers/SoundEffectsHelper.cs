using UnityEngine;

public class SoundEffectsHelper : MonoBehaviour
{
    public static SoundEffectsHelper Instance;

    public AudioClip playerShotSound;
    public AudioClip playerExplosionSound;
    public AudioClip enemyExplosionSound;

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple instances of SoundEffectsHelper!");
        }
        Instance = this;
    }

    public void MakePlayerShotSound()
    {
        MakeSound(playerShotSound);
    }

    public void MakePlayerExplosionSound()
    {
        MakeSound(playerExplosionSound);
    }

    public void MakeEnemyExplosionSound()
    {
        MakeSound(enemyExplosionSound);
    }

    private void MakeSound(AudioClip originalClip)
    {
        AudioSource.PlayClipAtPoint(originalClip, transform.position);
    }
}