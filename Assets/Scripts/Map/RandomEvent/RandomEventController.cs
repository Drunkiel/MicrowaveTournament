using UnityEngine;
using Photon.Pun;

public class RandomEventController : MonoBehaviour
{
    public int randomNum;

    public EventVoids _eventVoids;
    public FlowOfTheGameController _gameController;

    public void DrawNumber()
    {
        if (!PhotonNetwork.IsMasterClient) return;

        int num = (int)Mathf.Round(Random.Range(0, 4));
        print(num);

        if (num == 3)
        {
            randomNum = (int)Mathf.Round(Random.Range(1, 5));
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
                        _eventVoids.ChangePlayersScale(0.3f);
                        break;

                    case 1:
                        _eventVoids.ChangeBallScale(0.4f);
                        break;

                    case 2:
                        _eventVoids.ChangePlayersScale(0.3f);
                        _eventVoids.ChangeBallScale(0.4f);
                        break;

                    //Making player or ball big
                    case 3:
                        _eventVoids.ChangePlayersScale(0.9f);
                        break;

                    case 4:
                        _eventVoids.ChangeBallScale(1f);
                        break;

                    case 5:
                        _eventVoids.ChangePlayersScale(0.9f);
                        _eventVoids.ChangeBallScale(1f);
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
}
