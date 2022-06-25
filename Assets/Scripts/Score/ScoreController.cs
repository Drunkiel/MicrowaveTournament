using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    public int scoreForPlayerOne;
    public int scoreForPlayerTwo;

    public TMP_Text score;
    public BallController ballController;

    //To reset
    public GateController[] gateControllers;
    public int num;

    void Update()
    {
        score.text = scoreForPlayerOne.ToString() + " | " + scoreForPlayerTwo.ToString();
        CheckWinner();
    }

    public void AddPoints(bool forOne)
    {
        if (forOne)
        {
            scoreForPlayerOne++;
        }
        else
        {
            scoreForPlayerTwo++;
        }
    }

    void CheckWinner()
    {
        if(scoreForPlayerOne >= 8)
        {
            num = -1;
            ResetLevel();
        }

        if(scoreForPlayerTwo >= 8)
        {
            num = 1;
            ResetLevel();
        }
    }

    void ResetLevel()
    {
        //Reseting gates
        for (int i = 0; i < gateControllers.Length; i++)
        {
            gateControllers[i].cooldown = gateControllers[i].resCooldown;
            gateControllers[i].actualStep = 0; 
            gateControllers[i].OpenGate();         
        }

        //Reseting score
        scoreForPlayerOne = 0;
        scoreForPlayerTwo = 0;

        //Reseting ball
        ballController.ResetBall(num);
    }
}
