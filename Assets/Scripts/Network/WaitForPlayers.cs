using UnityEngine;
using Photon.Pun;
using DiscordPresence;

public class WaitForPlayers : MonoBehaviour
{
    public bool isEnoughtPlayers;
    public GameObject UI;

    PhotonView view;
    BallController _ballController;
    public ScoreController _scoreController;

    void Start()
    {
        view = GetComponent<PhotonView>();
        _ballController = GameObject.FindGameObjectWithTag("Ball").GetComponent<BallController>();
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
            PresenceManager.UpdatePresence(detail: "Waiting for players 1/2");
        }
        else
        {
            view.RPC("StartTime", RpcTarget.AllBuffered);
        }
    }

    void StopTime()
    {
        _ballController.StopBall();
        UI.SetActive(true);
    }

    [PunRPC]
    void StartTime()
    {
        UI.SetActive(false);
        _scoreController.ResetGateAndBall();
        _ballController.FirstRound();
        Destroy(GetComponent<WaitForPlayers>());
        PresenceManager.UpdatePresence(detail: "During the game");
    }
}
