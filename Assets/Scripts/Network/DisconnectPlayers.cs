using UnityEngine;
using Photon.Pun;

public class DisconnectPlayers : MonoBehaviour 
{
    [PunRPC]
    public void Disconnect()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            for (int i = 1; i < PhotonNetwork.CountOfPlayers; i++)
            {
                print(PhotonNetwork.PlayerList[i]);
                PhotonNetwork.CloseConnection(PhotonNetwork.PlayerList[i]);
            }
        }

        PhotonNetwork.LeaveRoom();
    }
}
