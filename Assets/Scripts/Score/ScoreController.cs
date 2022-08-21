using UnityEngine;
using Photon.Pun;
using TMPro;

public class ScoreController : MonoBehaviourPunCallbacks
{
    public int scoreForPlayerOne;
    public int scoreForPlayerTwo;
    public TMP_Text score;

    public int PlayerOneWinnedMaps;
    public int PlayerTwoWinnedMaps;

    public GameState _gameState;
    public ViewersController _viewersController;
    PhotonView view;

    //To animate
    SpawnText _spawnText;

    //After game win
    public TMP_Text team;

    void Start()
    {
        _spawnText = GetComponent<SpawnText>();
        view = GetComponent<PhotonView>();
    }

    void Update()
    {
        score.text = scoreForPlayerOne.ToString() + " | " + scoreForPlayerTwo.ToString();
        if (scoreForPlayerOne >= 4 || scoreForPlayerTwo >= 4) view.RPC("CheckRoundWinner", RpcTarget.AllBuffered);
        if (PlayerOneWinnedMaps == 3 || PlayerTwoWinnedMaps == 3) view.RPC("CheckGameWinner", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void AddPoints(bool forOne)
    {
        if (forOne)
        {
            scoreForPlayerOne++;
            _gameState.num = -1;
            _spawnText.BlueScored();
        }
        else
        {
            scoreForPlayerTwo++;
            _gameState.num = 1;
            _spawnText.RedScored();
        }

        view.RPC("ResetGateAndBall", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void ResetGateAndBall()
    {
        //Gate reset
        _gameState.ResetGate();

        //Reset ball after getting point
        _gameState.ResetBall(_gameState.num);
    }

    [PunRPC]
    void CheckRoundWinner()
    {
        if (scoreForPlayerOne >= 4)
        {
            _gameState.num = -1;
            PlayerOneWinnedMaps++;
        }

        if (scoreForPlayerTwo >= 4)
        {
            _gameState.num = 1;
            PlayerTwoWinnedMaps++;
        }

        view.RPC("SpawnViewers", RpcTarget.AllBuffered);
        view.RPC("RoundWinner", RpcTarget.AllBuffered);
    }

    [PunRPC]
    void CheckGameWinner()
    {
        if (PlayerOneWinnedMaps == 3)
        {
            team.color = new Color32(144, 15, 16, 255);
            team.text = "Team blue";
        }

        if (PlayerTwoWinnedMaps == 3)
        {
            team.color = new Color32(144, 15, 16, 255);
            team.text = "Team red";
        }

        view.RPC("GameWinner", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void NextGameBTN()
    {
        view.RPC("DespawnViewers", RpcTarget.AllBuffered);
    }

    [PunRPC]
    void RoundWinner()
    {
        _gameState.RoundWin();
    }

    [PunRPC]
    void GameWinner()
    {
        _gameState.GameWin();
    }

    [PunRPC]
    void DespawnViewers()
    {
        _viewersController.DespawnViewers();
    }

    [PunRPC]
    void SpawnViewers()
    {
        _viewersController.SpawnViewers();
    }
}