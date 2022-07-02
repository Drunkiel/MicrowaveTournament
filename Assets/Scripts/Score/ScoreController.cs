using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    public int scoreForPlayerOne;
    public int scoreForPlayerTwo;

    public TMP_Text score;
    public BallController ballController;
    public RandomEventController eventController;

    //To reset
    public GameObject[] players;
    public GateController[] gateControllers;
    public int num;
    public MapPicker mapPicker;

    //To animate
    SpawnText spawnText;

    void Start()
    {
        spawnText = GetComponent<SpawnText>();
    }

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
            num = -1;
            spawnText.BlueScored();
        }
        else
        {
            scoreForPlayerTwo++;
            num = 1;
            spawnText.RedScored();
        }

        //Gate reset
        ResetGate();

        //Reset ball after getting point
        ballController.ResetBall(num);
    }

    void CheckWinner()
    {
        if (scoreForPlayerOne >= 4)
        {
            num = -1;
        }

        if (scoreForPlayerTwo >= 4)
        {
            num = 1;
        }

        if (scoreForPlayerOne >= 4 || scoreForPlayerTwo >= 4)
        {
            ResetLevel();
            eventController.DrawNumber();
            eventController.PickEvent();
            mapPicker.PickMap();
        }

        eventController.eventVoids.FindBall();
    }

    void ResetGate()
    {
        for (int i = 0; i < gateControllers.Length; i++)
        {
            gateControllers[i].cooldown = gateControllers[i].resCooldown;
            gateControllers[i].actualStep = 0;
            gateControllers[i].OpenGate();
        }
    }

    void ResetLevel()
    {
        //Gate reset
        ResetGate();

        //Reseting players positions and rotation
        players[0].transform.position = new Vector3(-7, 0, 0);
        players[1].transform.position = new Vector3(7, 0, 0);

        players[0].transform.rotation = Quaternion.Euler(0, 90, 0);
        players[1].transform.rotation = Quaternion.Euler(0, -90, 0);

        //Reseting score
        scoreForPlayerOne = 0;
        scoreForPlayerTwo = 0;

        //Reseting ball
        ballController.ResetBall(num);
    }
}