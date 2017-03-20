using UnityEngine.Networking;

public class Shot : NetworkBehaviour
{
    public int damage = 1;

    [SyncVar]
    public bool isEnemyShot;
}