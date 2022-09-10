using System.Collections;
using System.IO;
using UnityEngine;
using Photon.Pun;

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
        if (!PhotonNetwork.IsMasterClient) Destroy(GetComponent<DesertMap>());

        players = GameObject.FindGameObjectsWithTag("Player");
        oldMultiplier = GameObject.FindGameObjectWithTag("MainCamera").transform.GetChild(0).GetComponent<ScoreController>()._gameState.num;
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
        }
        else if (multiplier == -1 && windParticle.transform.position.x <= 0 && !isParticle)
        {
            CreateParticle(-multiplier, multiplier);
        }
    }

    [PunRPC]
    void CreateParticle(float num1, float num2)
    {
        PhotonNetwork.Instantiate(Path.Combine("Particles", windParticle.name), new Vector3(windParticle.transform.position.x * multiplier, windParticle.transform.position.y, -3f), Quaternion.Euler(0, 90 * multiplier, 0));
        isParticle = true;
    }

    private IEnumerator RestTime()
    {
        yield return new WaitForSeconds(resCooldown);
        multiplier = -oldMultiplier;
    }
}