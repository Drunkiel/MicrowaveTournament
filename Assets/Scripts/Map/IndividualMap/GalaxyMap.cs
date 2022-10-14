using UnityEngine;

public class GalaxyMap : MonoBehaviour
{
    public float cooldown;
    public int minSpeed;
    public int maxSpeed;
    public float ballMultiplier;
    public float playerMultiplier = 1;

    public GameObject[] players;

    WindController _windController;

    // Start is called before the first frame update
    void Start()
    {
        _windController = GetComponent<WindController>();
        players = _windController.players;
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown <= 0)
        {
            ChangeWindSpeed();
            cooldown = 8;
        }
        else
        {
            cooldown -= Time.deltaTime;
        }

        CheckPlayersBall();
        _windController.Wind(1, ballMultiplier, playerMultiplier);
    }

    void ChangeWindSpeed()
    {
        int newSpeed = Random.Range(minSpeed, maxSpeed);
        _windController.windYSpeed = newSpeed;
    }

    void CheckPlayersBall()
    {
        foreach (GameObject player in players)
        {
            if(player.transform.position.y <= -2.5f)
            {
                playerMultiplier += 0.1f;
            }
            else
            {
                playerMultiplier = 1;
            }
        }

        if(_windController.ball.transform.position.y <= -2.5f)
        {
            ballMultiplier = 0.2f;
        }
        else
        {
            ballMultiplier = 0.1f;
        }
    }
}
