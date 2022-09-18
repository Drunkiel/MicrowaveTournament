using UnityEngine;
using Photon.Pun;

public class RandomEventController : MonoBehaviour
{
    public int randomNum;

    PhotonView view;
    public EventVoids _eventVoids;
    public FlowOfTheGameController _gameController;

    void Start()
    {
        view = GetComponent<PhotonView>();
    }

    public void DrawNumber()
    {
        if (!PhotonNetwork.IsMasterClient) return;

        int num = (int)Mathf.Round(Random.Range(0, 4));
        num = 3;

        if (num == 3)
        {
            randomNum = (int)Mathf.Round(Random.Range(1, 6));
            randomNum = 2;
        }
        else
        {
            randomNum = 0;
        }
    }

    public void PickEvent()
    {
        if (!PhotonNetwork.IsMasterClient) return;

        switch (randomNum)
        {
            case 0:
                _gameController.ResetObjects();
                break;

            case 1:
                int number = (int)Mathf.Round(Random.Range(0, 6));

                switch (number)
                {
                    //Making player or ball small
                    case 0:
                        view.RPC("ChangePlayerBallScale", RpcTarget.AllBuffered, 0.3f, 0.8f);
                        break;

                    case 1:
                        view.RPC("ChangePlayerBallScale", RpcTarget.AllBuffered, 0.6f, 0.4f);
                        break;

                    case 2:
                        view.RPC("ChangePlayerBallScale", RpcTarget.AllBuffered, 0.3f, 0.4f);
                        break;

                    //Making player or ball big
                    case 3:
                        view.RPC("ChangePlayerBallScale", RpcTarget.AllBuffered, 0.9f, 0.8f);
                        break;

                    case 4:
                        view.RPC("ChangePlayerBallScale", RpcTarget.AllBuffered, 0.6f, 1f);
                        break;

                    case 5:
                        view.RPC("ChangePlayerBallScale", RpcTarget.AllBuffered, 0.9f, 1f);
                        break;
                }
                break;

            //Changing something in map
            case 2:
                DestroyGatesBall(true, false);
                _eventVoids.WoodenGates();
                break;

            case 3:
                DestroyGatesBall(true, true);
                _eventVoids.ExplosiveMode();
                break;

            case 4:
                DestroyGatesBall(false, true);
                _eventVoids.DiscoMode();
                break;

            case 5:
                DestroyGatesBall(true, false);
                _eventVoids.BaketballGates();
                break;
        }

        _gameController.SetGatesToParent();
    }

    public void DestroyGatesBall(bool gates, bool ball)
    {
        if (gates)
        {
            for (int i = 0; i < _eventVoids.actualGates.Length; i++)
            {
                PhotonNetwork.Destroy(_eventVoids.actualGates[i]);
            }
        }

        if (ball)
        {
            PhotonNetwork.Destroy(_eventVoids.ball);
        }
    }

    [PunRPC]
    void ChangePlayerBallScale(float playerScale, float ballScale)
    {
        _eventVoids.ChangePlayersScale(playerScale);
        _eventVoids.ChangeBallScale(ballScale);
    }
}
