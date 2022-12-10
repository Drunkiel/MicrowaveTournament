using Photon.Pun;
using UnityEngine;

[System.Serializable]
public class GameState
{
    public BallController _ballController;
    public RandomEventController _eventController;
    public FlowOfTheGameController _gameController;
    public ScoreController _scoreController;
    public GameWinMenuController _gameWinMenu;
    public LoadingScreen _loadingScreen;
    public MapPicker _mapPicker;

    //To reset
    public GameObject[] players;
    public GameObject[] actualGates;
#nullable enable
    public GateController[]? _gateControllers;
    public WoodenGateController[]? _woodenGates;
#nullable disable
    public int num;

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
            if (actualGates[i].transform.childCount > 1)
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

        _gameController.SetGatesToParent();
    }

    [PunRPC]
    public void ResetBall(int goLeft)
    {
        //Getting ball
        _eventController._eventVoids.ball = _gameController.FindBall();
        _ballController = _gameController._eventVoids.ball.GetComponent<BallController>();

        //Setting ball to start round
        _ballController.transform.position = new Vector2(0, 2.2f);
        _ballController.StopBall();
        _ballController.rgBody.AddForce(new Vector2(_ballController.startVector.x * goLeft, 0), ForceMode.Impulse);
    }

    public void ResetPlayers()
    {
        GetPlayers();

        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].transform.parent) players[i].transform.parent = null;
        }

        players[0].transform.position = new Vector2(-7, 0);
        players[0].transform.rotation = Quaternion.Euler(0, 90, 0);

        if (players.Length > 1)
        {
            players[1].transform.position = new Vector2(7, 0);
            players[1].transform.rotation = Quaternion.Euler(0, -90, 0);
        }
    }

    void GetGates()
    {
        _gameController.FindGates();
        GameObject[] allGates = _eventController._eventVoids.actualGates;

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

    void GetPlayers()
    {
        _gameController.FindPlayers();
        players = _eventController._eventVoids.players;
    }

    [PunRPC]
    public void ResetLevel()
    {
        //Reseting score
        _scoreController.scoreForPlayerOne = 0;
        _scoreController.scoreForPlayerTwo = 0;

        if (!PhotonNetwork.IsMasterClient) return;

        _gameController.ResetObjects();

        //Gate reset
        ResetGate();

        //Reseting players positions, rotation and doors
        ResetPlayers();
        ResetDoors();

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
            if (doors[i] == null) return;

            doors[i] = players[i].GetComponent<DoorController>();
            isBallPicked[i] = doors[i].isBallPicked;

            if (isBallPicked[i]) doors[i].AutoShoot();
        }
    }

    [PunRPC]
    public void GameWin()
    {
        _scoreController.playerOneWinnedMaps = 0;
        _scoreController.playerTwoWinnedMaps = 0;
        Cursor.visible = true;

        _gameWinMenu.OpenMenu();
    }

    [PunRPC]
    public void RoundWin()
    {
        //Cleaning scene
        _loadingScreen.StartCoroutine("Load_Start");
        _gameController.DestroyParticles();
        ResetLevel();

        //Setting new round
        if (PhotonNetwork.IsMasterClient)
        {
            _eventController.DrawNumber();
            _eventController.PickEvent();
            _mapPicker.PickMap();
        }
    }
}
