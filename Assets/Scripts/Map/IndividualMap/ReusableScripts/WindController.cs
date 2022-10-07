using UnityEngine;

public class WindController : MonoBehaviour
{
    public GameObject[] players;
    public Rigidbody ball;

    public float windXSpeed;
    public float windYSpeed;

    // Start is called before the first frame update
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        ball = GameObject.FindGameObjectWithTag("Ball").GetComponent<Rigidbody>();
    }

    public void Wind(int multiplier)
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].GetComponent<Rigidbody>().AddForce(new Vector2(windXSpeed * multiplier, windYSpeed));
        }

        ball.AddForce(new Vector2(windXSpeed * multiplier, windYSpeed));
    }
}
