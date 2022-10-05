using UnityEngine;
using Photon.Pun;
using TMPro;

public class ScoreController : MonoBehaviourPunCallbacks
{
    public int scoreForPlayerOne;
    public int scoreForPlayerTwo;
    public TMP_Text[] score;

    public int PlayerOneWinnedMaps;
    public int PlayerTwoWinnedMaps;

    public GameObject goalParticle;
    public GameObject gameWinParticle;

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
        score[0].text = scoreForPlayerOne.ToString();
        score[1].text = scoreForPlayerTwo.ToString();

        if (scoreForPlayerOne >= 4 || scoreForPlayerTwo >= 4) view.RPC("CheckRoundWinner", RpcTarget.AllBuffered);
        if (PlayerOneWinnedMaps == 4 || PlayerTwoWinnedMaps == 4) view.RPC("CheckGameWinner", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void AddPoints(bool forOne)
    {
        if (forOne)
        {
            scoreForPlayerOne++;
            _gameState.num = -1;
            _spawnText.BlueScored();
            Instantiate(goalParticle, new Vector3(-9, -1.7f, 0), Quaternion.identity);
        }
        else
        {
            scoreForPlayerTwo++;
            _gameState.num = 1;
            _spawnText.RedScored();
            Instantiate(goalParticle, new Vector3(9, -1.7f, 0), Quaternion.Euler(0, 180, 0));
        }

        view.RPC("ResetGateBallDoors", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void ResetGateBallDoors()
    {
        //Gate reset
        _gameState.ResetGate();

        //ResetDoors
        _gameState.ResetDoors();

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
        if (PlayerOneWinnedMaps == 4)
        {
            team.color = new Color32(15, 45, 144, 255);
            team.text = "Team blue";
        }

        if (PlayerTwoWinnedMaps == 4)
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
        Instantiate(gameWinParticle);
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