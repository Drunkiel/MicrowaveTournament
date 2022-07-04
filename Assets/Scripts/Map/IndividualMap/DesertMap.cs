using System.Collections;
using UnityEngine;

public class DesertMap : MonoBehaviour
{
    public float windSpeed;
    public int multiplier;
    public int oldMultiplier;
    public int previousMultiplier;
    public bool isParticle;

    public float cooldown;
    public float resCooldown;

    public GameObject[] players;
    public GameObject windParticle;
    public Rigidbody ball;

    // Start is called before the first frame update
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
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

        if ((num >= -0.5f && num <= 0.5f) || previousMultiplier != 0)
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
            ball = GameObject.FindGameObjectWithTag("Ball").GetComponent<Rigidbody>();
        }

        previousMultiplier = multiplier;
    }

    void Wind()
    {
        if (multiplier != 0)
        {
            for (int i = 0; i < players.Length; i++)
            {
                players[i].GetComponent<Rigidbody>().AddForce(new Vector2(windSpeed * multiplier, 4));
            }

            WindAnimation();

            ball.AddForce(new Vector2(windSpeed / 10 * multiplier, 0)); // Add windSpeed / 10
        }
        else
        {
            isParticle = false;
        }
    }

    void WindAnimation()
    {
        if (multiplier == 1 && windParticle.transform.position.x <= 0 && !isParticle)
        {
            CreateParticle(multiplier, multiplier);
            print("1");
        }
        else if (multiplier == 1 && windParticle.transform.position.x >= 0 && !isParticle)
        {
            CreateParticle(-multiplier, -multiplier);
            print("2");
        }
        else if (multiplier == -1 && windParticle.transform.position.x >= 0 && !isParticle)
        {
            CreateParticle(multiplier, multiplier);
            print("3");
        }
        else if (multiplier == -1 && windParticle.transform.position.x <= 0 && !isParticle)
        {
            CreateParticle(-multiplier, multiplier);
            print("4");
        }
    }

    void CreateParticle(float num1, float num2)
    {
        Instantiate(windParticle, new Vector3(windParticle.transform.position.x * multiplier, windParticle.transform.position.y, windParticle.transform.position.z), Quaternion.Euler(0, 90 * multiplier, 0), this.gameObject.transform);
        isParticle = true;
    }

    private IEnumerator RestTime()
    {
        yield return new WaitForSeconds(resCooldown);
        multiplier = -oldMultiplier;
    }
}