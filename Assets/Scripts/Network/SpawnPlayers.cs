using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;
    public int playersCount;

    // Start is called before the first frame update
    void Start()
    {
        playersCount = PhotonNetwork.CurrentRoom.PlayerCount;
        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        float num = 1;

        if (playersCount == 1)
        {
            num = 1;
        }
        else if (playersCount == 2)
        {
            num = -1;
        }

        if (playersCount <= 2)
        {
            PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(-7 * num, 0, 0), Quaternion.Euler(0, 90 * num, 0));
        }
    }
}
