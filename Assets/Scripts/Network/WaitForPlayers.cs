using UnityEngine;
using Photon.Pun;

public class WaitForPlayers : MonoBehaviour
{
    public GameObject UI;

    PhotonView view;
    BallController _ballController;
    public ScoreController _scoreController;
    public FlowOfTheGameController _gameController;

    void Start()
    {
        view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
            {
                view.RPC("StartTime", RpcTarget.AllBuffered);
            }
            else if(1 == 0)
            {
                view.RPC("StopTime", RpcTarget.AllBuffered);
            }
        }
    }

    [PunRPC]
    void StopTime()
    {
        _ballController.StopBall();
        UI.SetActive(true);
    }

    [PunRPC]
    void StartTime()
    {
        UI.SetActive(false);
        _scoreController.ResetGateBallDoors();
        _ballController.FirstRound();
        Destroy(GetComponent<WaitForPlayers>());
    }
}
