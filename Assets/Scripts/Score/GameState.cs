using UnityEngine;
using Photon.Pun;

[System.Serializable]
public class GameState
{
    public BallController _ballController;
    public RandomEventController _eventController;
    public ScoreController _scoreController;
    public GameWinMenuController _gameWinMenu;
    public LoadingScreen _loadingScreen;

    //To reset
    public GameObject[] players;
    public GameObject[] actualGates;
#nullable enable
    public GateController[]? _gateControllers;
    public WoodenGateController[]? _woodenGates;
#nullable disable
    public int num;
    public MapPicker mapPicker;

    [PunRPC]
    public void ResetGate()
    {
        GetGates();

        for (int i = 0; i < actualGates.Length; i++)
        {
            if (actualGates[i].TryGetComponent(out GateController gateController))
            {
                _gateControllers[i] = gateController;
            }

            //Checking if there are parts
            if (actualGates[i].transform.childCount != 0)
            {
                Transform[] childs = new Transform[actualGates.Length * actualGates[0].transform.childCount];

                //Assigment to childs
                for (int j = 0; j < actualGates.Length; j++)
                {
                    for (int k = 0; k < actualGates[j].transform.childCount; k++)
                    {
                        childs[k] = actualGates[0].transform.GetChild(k);
                        childs[k + 2] = actualGates[1].transform.GetChild(k);
                    }
                }

                //Assignment to wooden gates
                for (int j = 0; j < childs.Length; j++)
                {
                    _woodenGates[j] = childs[j].GetComponent<WoodenGateController>();
                }
            }
        }

        if (_gateControllers[0] != null)
        {
            for (int i = 0; i < _gateControllers.Length; i++)
            {
                _gateControllers[i].cooldown = _gateControllers[i].resCooldown;
                _gateControllers[i].actualStep = 0;
                _gateControllers[i].OpenGate();
            }
        }

        if (_woodenGates[0] != null)
        {
            for (int i = 0; i < _woodenGates.Length; i++)
            {
                _woodenGates[i].defects = 0;
                _woodenGates[i].Injuries();
            }
        }

        _eventController._eventVoids.SetGatesToParent();
    }

    [PunRPC]
    public void ResetBall(int goLeft)
    {
        _eventController._eventVoids.FindBall();

        _ballController.transform.position = new Vector3(0, 2.2f, -0.3f);
        _ballController.StopBall();
        _ballController.StartBall();
        _ballController.rgBody.AddForce(new Vector3(_ballController.startVector.x * goLeft, 0, 0), ForceMode.Impulse);
    }

    void GetGates()
    {
        _eventController._eventVoids.FindGates();
        GameObject[] allGates = _eventController._eventVoids.gates;

        if (allGates.Length == 2)
        {
            actualGates = allGates;
        }
        else
        {
            actualGates[0] = allGates[0].transform.parent.gameObject;
            actualGates[1] = allGates[2].transform.parent.gameObject;
        }
    }

    [PunRPC]
    public void ResetLevel()
    {
        //Gate reset
        ResetGate();

        //Reseting players positions and rotation
        players[0].transform.position = new Vector3(-7, 0, 0);
        players[1].transform.position = new Vector3(7, 0, 0);

        players[0].transform.rotation = Quaternion.Euler(0, 90, 0);
        players[1].transform.rotation = Quaternion.Euler(0, -90, 0);

        //Reseting score
        _scoreController.scoreForPlayerOne = 0;
        _scoreController.scoreForPlayerTwo = 0;

        //Reseting ball
        ResetBall(num);
    }

    [PunRPC]
    public void ResetDoors()
    {
        bool[] isBallPicked = new bool[2];
        DoorController[] doors = new DoorController[2];

        for (int i = 0; i < players.Length; i++)
        {
            doors[i] = players[i].GetComponent<DoorController>();
            isBallPicked[i] = doors[i].isBallPicked;

            if (isBallPicked[i]) doors[i].AutoShoot();
        }
    }

    [PunRPC]
    public void GameWin()
    {
        _scoreController.PlayerOneWinnedMaps = 0;
        _scoreController.PlayerTwoWinnedMaps = 0;

        _gameWinMenu.OpenMenu();
    }

    [PunRPC]
    public void RoundWin()
    {
        _loadingScreen.StartCoroutine("Load_Start");
        ResetDoors();
        ResetLevel();
        if (PhotonNetwork.IsMasterClient)
        {
            _eventController.DrawNumber();
            _eventController.PickEvent();
        }
        mapPicker.PickMap();

        _eventController._eventVoids.FindBall();
    }
}
