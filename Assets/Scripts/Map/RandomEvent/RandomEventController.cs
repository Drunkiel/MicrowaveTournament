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

        int num = (int)Mathf.Round(Random.Range(0, 3));

        if (num == num)
        {
            randomNum = (int)Mathf.Round(Random.Range(1, 6));
        }
        else
        {
            randomNum = 0;
        }
    }

    public void PickEvent()
    {
        switch (randomNum)
        {
            case 0:
                print("Nothing to change");
                break;

            case 1:
                int number = (int)Mathf.Round(Random.Range(0, 6));

                switch (number)
                {
                    //Making player or ball small
                    case 0:
                        ChangePlayerBallScale(0.3f, 0.8f);
                        break;

                    case 1:
                        ChangePlayerBallScale(0.6f, 0.4f);
                        break;

                    case 2:
                        ChangePlayerBallScale(0.3f, 0.4f);
                        break;

                    //Making player or ball big
                    case 3:
                        ChangePlayerBallScale(0.9f, 0.8f);
                        break;

                    case 4:
                        ChangePlayerBallScale(0.6f, 1f);
                        break;

                    case 5:
                        ChangePlayerBallScale(0.9f, 1f);
                        break;
                }
                break;

            //Changing something in map
            case 2:
                _eventVoids.WoodenGates();
                break;

            case 3:
                _eventVoids.ExplosiveMode();
                break;

            case 4:
                _eventVoids.DiscoMode();
                break;

            case 5:
                _eventVoids.BaketballGates();
                break;
        }

        _gameController.SetGatesToParent();
    }

    [PunRPC]
    void ChangePlayerBallScale(float playerScale, float ballScale)
    {
        _eventVoids.ChangePlayersScale(playerScale);
        _eventVoids.ChangeBallScale(ballScale);
    }
}
