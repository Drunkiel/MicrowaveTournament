using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    public int scoreForPlayerOne;
    public int scoreForPlayerTwo;
    public TMP_Text score;

    public int PlayerOneWinnedMaps;
    public int PlayerTwoWinnedMaps;

    public GameState _gameState;

    //To animate
    SpawnText _spawnText;

    void Start()
    {
        _spawnText = GetComponent<SpawnText>();
    }

    void Update()
    {
        score.text = scoreForPlayerOne.ToString() + " | " + scoreForPlayerTwo.ToString();
        if (scoreForPlayerOne >= 4 || scoreForPlayerTwo >= 4) CheckLevelWinner();
        if (PlayerOneWinnedMaps == 3 || PlayerTwoWinnedMaps == 3) CheckGameWinner();
    }

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

        //Gate reset
        _gameState.ResetGate();

        //Reset ball after getting point
        _gameState.ResetBall(_gameState.num);
    }

    void CheckLevelWinner()
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

        _gameState.RoundWin();
    }

    void CheckGameWinner()
    {
        if (PlayerOneWinnedMaps == 3)
        {
            print("blue wins");
        }

        if (PlayerTwoWinnedMaps == 3)
        {
            print("red wins");
        }

        _gameState.GameWin();
    }
}