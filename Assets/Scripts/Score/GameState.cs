using UnityEngine;
using Photon.Pun;

[System.Serializable]
public class GameState
{
    public BallController _ballController;
    public RandomEventController _eventController;
    public ScoreController _scoreController;
    public GameWinMenuController _gameWinMenu;

    //To reset
    public GameObject[] players;
#nullable enable
    public GateController[]? _gateControllers;
    public WoodenGateController[]? _woodenGates;
#nullable disable
    public int num;
    public MapPicker mapPicker;

    [PunRPC]
    public void ResetGate()
    {
        _eventController.eventVoids.FindGates();
        GameObject[] gates = _eventController.eventVoids.gates;

        for (int i = 0; i < gates.Length; i++)
        {
            if (gates[i].TryGetComponent(out GateController gateController))
            {
                _gateControllers[i] = gateController;
            }

            if (gates[i].TryGetComponent(out WoodenGateController woodenGate))
            {
                _woodenGates[i] = woodenGate;
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
    }

    [PunRPC]
    public void ResetBall(int goLeft)
    {
        _eventController.eventVoids.FindBall();

        _ballController.transform.position = new Vector3(0, 2.2f, -0.3f);
        _ballController.StopBall();
        _ballController.rgBody.AddForce(new Vector3(_ballController.startVector.x * goLeft, 0, 0), ForceMode.Impulse);
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
    public void GameWin()
    {
        _scoreController.PlayerOneWinnedMaps = 0;
        _scoreController.PlayerTwoWinnedMaps = 0;

        _gameWinMenu.OpenMenu();
    }

    [PunRPC]
    public void RoundWin()
    {
        ResetLevel();
        _eventController.DrawNumber();
        _eventController.PickEvent();
        mapPicker.PickMap();

        _eventController.eventVoids.FindBall();
    }
}
