using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    public int scoreForPlayerOne;
    public int scoreForPlayerTwo;

    public TMP_Text score;
    public BallController ballController;

    void Update()
    {
        score.text = scoreForPlayerOne.ToString() + " | " + scoreForPlayerTwo.ToString();
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

        ballController.ResetBall();
    }
}
