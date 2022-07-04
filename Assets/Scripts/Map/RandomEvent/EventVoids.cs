using UnityEngine;

public class EventVoids : MonoBehaviour
{
    public GameObject[] players;
    public GameObject ball;
    public GameObject defaultBall;
    public GameObject explosiveBall;

    public ScoreController scoreController;

    void Start()
    {
        FindBall();
    }

    public void FindBall()
    {
        ball = GameObject.FindGameObjectWithTag("Ball");
        scoreController.ballController = ball.GetComponent<BallController>();
    }

    public void ResetObjects()
    {
        ChangePlayersScale(0.6f);
        ChangeBallScale(0.8f);

        if (ball.TryGetComponent<ExplosiveBallController>(out ExplosiveBallController ballController))
        {
            Destroy(ball);
            Instantiate(defaultBall, Vector3.zero, Quaternion.identity);
        }
    }

    public void ChangePlayersScale(float scale)
    {
        foreach (GameObject player in players)
        {
            player.transform.localScale = new Vector3(scale, scale, scale);
        }
    }

    public void ChangeBallScale(float scale)
    {
        ball.transform.localScale = new Vector3(scale, scale, scale);
    }

    public void ExplosiveMode()
    {
        Destroy(ball);
        Instantiate(explosiveBall, Vector3.zero, Quaternion.identity);
    }
}
