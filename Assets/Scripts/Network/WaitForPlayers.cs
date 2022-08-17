using UnityEngine;
using Photon.Pun;

public class WaitForPlayers : MonoBehaviour
{
    public bool isEnoughtPlayers;
    public GameObject UI;

    PhotonView view;
    BallController _ballController;

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
        _ballController.FirstRound();
        Destroy(GetComponent<WaitForPlayers>());
    }
}
