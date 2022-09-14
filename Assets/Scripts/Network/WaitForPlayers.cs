using UnityEngine;
using Photon.Pun;

public class WaitForPlayers : MonoBehaviour
{
    public bool isEnoughtPlayers;
    public GameObject UI;

    PhotonView view;
    BallController _ballController;
    public ScoreController _scoreController;
    public FlowOfTheGameController _gameController;

    void Start()
    {
        view = GetComponent<PhotonView>();
        GetBall();
    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.CountOfPlayers == 2)
        {
            isEnoughtPlayers = true;
        }
        else
        {
            isEnoughtPlayers = false;
        }

        if (!isEnoughtPlayers)
        {
            StopTime();
        }
        else
        {
            view.RPC("StartTime", RpcTarget.AllBuffered);
        }
    }

    void StopTime()
    {
        GetBall();
        _ballController.StopBall();
        UI.SetActive(true);
    }

    [PunRPC]
    void StartTime()
    {
        GetBall();
        UI.SetActive(false);
        _scoreController.ResetGateBallDoors();
        _ballController.FirstRound();
        Destroy(GetComponent<WaitForPlayers>());
    }

    void GetBall()
    {
        _gameController.FindBall();
        _ballController = _gameController._eventVoids.ball.GetComponent<BallController>();
    }
}
