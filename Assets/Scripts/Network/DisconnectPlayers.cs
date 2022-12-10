using UnityEngine;
using Photon.Pun;

public class DisconnectPlayers : MonoBehaviour 
{
    [PunRPC]
    public void Disconnect()
    {
        PhotonNetwork.LeaveRoom();
    }
}
