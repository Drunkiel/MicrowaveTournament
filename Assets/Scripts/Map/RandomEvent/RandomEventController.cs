using UnityEngine;
using Photon.Pun;

public class RandomEventController : MonoBehaviour
{
    public int randomNum;

    public EventVoids _eventVoids;

    public void DrawNumber()
    {
        if (!PhotonNetwork.IsMasterClient) return;

        int num = (int)Mathf.Round(Random.Range(1, 5));

        if (num == 4)
        {
            randomNum = (int)Mathf.Round(Random.Range(1, 3));
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
                _eventVoids.ResetObjects();
                break;

            case 1:
                int num = (int)Mathf.Round(Random.Range(1, 7));

                switch (num)
                {
                    //Making player or ball small
                    case 1:
                        _eventVoids.ChangePlayersScale(0.3f);
                        break;

                    case 2:
                        _eventVoids.ChangeBallScale(0.4f);
                        break;

                    case 3:
                        _eventVoids.ChangePlayersScale(0.3f);
                        _eventVoids.ChangeBallScale(0.4f);
                        break;

                    //Making player or ball big
                    case 4:
                        _eventVoids.ChangePlayersScale(0.9f);
                        break;

                    case 5:
                        _eventVoids.ChangeBallScale(1f);
                        break;

                    case 6:
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
        }

        _eventVoids.SetGatesToParent();
    }
}
