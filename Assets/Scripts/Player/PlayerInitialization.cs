using Unity.Netcode;

public class PlayerInitialization : NetworkBehaviour
{
    public int playerIndex;

    [ServerRpc(RequireOwnership = false)]
    public void CheckIfHostServerRpc()
    {
        for (int i = 0; i < NetworkManager.ConnectedClientsIds.Count; i++)
        {
            playerIndex = NetworkManager.ConnectedClientsIds.Count;
        }
    }
}
