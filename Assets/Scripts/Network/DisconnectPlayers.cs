using UnityEngine;
using Photon.Pun;

public class DisconnectPlayers : MonoBehaviour 
{
    [PunRPC]
    public void Disconnect()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if(PhotonNetwork.CountOfPlayers >= 2)
            {
                GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

                for (int i = 1; i < players.Length; i++)
                {
                    PhotonNetwork.CloseConnection(PhotonNetwork.PlayerList[i]);
                }
            }
        }

        PhotonNetwork.LeaveRoom();
    }
}
