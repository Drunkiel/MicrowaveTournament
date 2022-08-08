using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject[] playerPrefabs;
    public int playersCount;

    // Start is called before the first frame update
    void Start()
    {
        playersCount = PhotonNetwork.CurrentRoom.PlayerCount;
        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        if (playersCount == 1)
        {
            PhotonNetwork.Instantiate(playerPrefabs[0].name, new Vector3(-7, 0, 0), Quaternion.Euler(0, 90, 0));
        }
        else if (playersCount == 2)
        {
            PhotonNetwork.Instantiate(playerPrefabs[1].name, new Vector3(7, 0, 0), Quaternion.Euler(0, -90, 0));
        }
    }
}
