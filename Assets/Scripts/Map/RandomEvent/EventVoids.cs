using UnityEngine;

public class EventVoids : MonoBehaviour
{
    public GameObject[] players;

    //Ball
    public GameObject ball;
    public GameObject defaultBall;
    public GameObject explosiveBall;

    //Gates
    public GameObject[] gates;
    public GameObject[] normalGates;
    public GameObject[] woodenGates;
    public Transform[] parents;

    public ScoreController scoreController;

    void Start()
    {
        FindBall();
        FindGates();
    }

    public void FindBall()
    {
        ball = GameObject.FindGameObjectWithTag("Ball");
        scoreController.ballController = ball.GetComponent<BallController>();
    }

    public void FindGates()
    {
        gates = GameObject.FindGameObjectsWithTag("Gate");
    }

    public void ResetObjects()
    {
        ChangePlayersScale(0.6f);
        ChangeBallScale(0.8f);

        if (ball.TryGetComponent(out ExplosiveBallController ballController))
        {
            Destroy(ball);
            Instantiate(defaultBall, Vector3.zero, Quaternion.identity);
        }

        for (int i = 0; i < gates.Length; i++)
        {
            if (gates[i].transform.childCount > 0 && gates[i].TryGetComponent(out WoodenGateController woodenGate))
            {
                Destroy(gates[i].transform.parent.gameObject);

                if (i < normalGates.Length)
                {
                    Instantiate(normalGates[i], Vector3.zero, Quaternion.identity, parents[i]);
                }
            }
            else
            {
                break;
            }
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

    public void WoodenGates()
    {
        for (int i = 0; i < gates.Length; i++)
        {
            Destroy(gates[i]);
            Instantiate(woodenGates[i], Vector3.zero, Quaternion.identity, parents[i]);
        }
    }
}
