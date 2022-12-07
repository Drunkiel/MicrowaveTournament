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

    public void Wind(float xMultiplier, float ballMultiplier, float playerMultiplier)
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].GetComponent<Rigidbody>().AddForce(new Vector2(windXSpeed * xMultiplier, windYSpeed * playerMultiplier));
        }
        
        ball.AddForce(new Vector2(windXSpeed * xMultiplier, windYSpeed * ballMultiplier));
    }
}
