using UnityEngine.Networking;

public class PlayerNetworkSetup : NetworkBehaviour
{
    public override void OnStartLocalPlayer()
    {
        GetComponent<Player>().enabled = true;
    }
}