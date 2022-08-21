using UnityEngine;
using Photon.Pun;

public class BuildingGame : MonoBehaviour
{
    public PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.CountOfPlayers == 1 || PhotonNetwork.CountOfPlayers >= 2)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            MultiplyVariables();
        }
    }

    void MultiplyVariables()
    {
        //Checking if not multiplied
        if (player.moveForce == 7.5f)
        {
            return;
        }
        else
        {
            player.moveForce *= 2.5f;
            player.rotateSpeed *= 2.5f;
        }
    }
}
