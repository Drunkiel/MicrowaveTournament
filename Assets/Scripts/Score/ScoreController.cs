using UnityEngine;
using Photon.Pun;
using TMPro;
using System.Collections;

public class ScoreController : MonoBehaviourPunCallbacks
{
    public int scoreForPlayerOne;
    public int scoreForPlayerTwo;
    public TMP_Text[] score;

    public int playerOneWinnedMaps;
    public int playerTwoWinnedMaps;

    public GameObject goalParticle;
    public GameObject gameWinParticle;

    public GameState _gameState;
    public CameraController _cameraController;
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
        if (playerOneWinnedMaps == 4 || playerTwoWinnedMaps == 4) view.RPC("CheckGameWinner", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void AddPoints(bool forOne)
    {
        if (forOne)
        {
            scoreForPlayerOne++;
            _gameState.num = -1;
            _spawnText.BlueScored();
            if(scoreForPlayerOne < 4) _cameraController.BlueGoal();
            Instantiate(goalParticle, new Vector3(9, -1.7f, 0), Quaternion.Euler(0, 180, 0));
        }
        else
        {
            scoreForPlayerTwo++;
            _gameState.num = 1;
            _spawnText.RedScored();
            if(scoreForPlayerTwo < 4) _cameraController.RedGoal();
            Instantiate(goalParticle, new Vector3(-9, -1.7f, 0), Quaternion.identity);
        }

        StartCoroutine("Goal");
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
            playerOneWinnedMaps++;
        }

        if (scoreForPlayerTwo >= 4)
        {
            _gameState.num = 1;
            playerTwoWinnedMaps++;
        }

        view.RPC("SpawnViewers", RpcTarget.AllBuffered);
        view.RPC("RoundWinner", RpcTarget.AllBuffered);
    }

    [PunRPC]
    void CheckGameWinner()
    {
        if (playerOneWinnedMaps == 4)
        {
            team.color = new Color32(15, 45, 144, 255);
            team.text = "Team blue";
        }

        if (playerTwoWinnedMaps == 4)
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
    void SpawnViewers()
    {
        _viewersController.SpawnViewers();
    }

    [PunRPC]
    void DespawnViewers()
    {
        _viewersController.DespawnViewers();
    }

    IEnumerator Goal()
    {
        _gameState._ballController.StopBall();

        yield return new WaitForSeconds(1.6f);

        view.RPC("ResetGateBallDoors", RpcTarget.AllBuffered);
    }
}