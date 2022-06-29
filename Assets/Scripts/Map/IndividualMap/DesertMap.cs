using System.Collections;
using UnityEngine;

public class DesertMap : MonoBehaviour
{
    public float windSpeed;
    public int multiplier;
    public int oldMultiplier;

    public float cooldown;
    public float resCooldown;

    public GameObject[] players;
    public Rigidbody ball;

    // Start is called before the first frame update
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        ball = GameObject.FindGameObjectWithTag("Ball").GetComponent<Rigidbody>();
        oldMultiplier = GameObject.FindGameObjectWithTag("MainCamera").transform.GetChild(0).GetComponent<ScoreController>().num;
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown <= 0)
        {
            DrawNumber();
            ChangeMultiplierValue();
            cooldown = resCooldown;
        }
        else
        {
            cooldown -= Time.deltaTime;
        }

        Wind();
    }

    void DrawNumber()
    {
        float num = Mathf.Round(Random.Range(-1, 1));

        if (num >= -0.5f && num <= 0.5f)
        {
            multiplier = 0;
        }
    }

    void ChangeMultiplierValue()
    {
        if (multiplier != 0)
        {
            oldMultiplier = multiplier;
            multiplier *= -1;
        }
        else
        {
            StartCoroutine("RestTime");
        }
    }

    void Wind()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].GetComponent<Rigidbody>().AddForce(new Vector2(windSpeed * multiplier, 8));
        }

        ball.AddForce(new Vector2(windSpeed / 10 * multiplier, 0));
    }

    private IEnumerator RestTime()
    {
        yield return new WaitForSeconds(resCooldown);
        multiplier = oldMultiplier;
    }
}