using System.IO;
using UnityEngine;
using Photon.Pun;

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
    public GameObject[] steelGates;

    //Gates parent
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
        scoreController._gameState._ballController = ball.GetComponent<BallController>();
    }

    public void FindGates()
    {
        gates = GameObject.FindGameObjectsWithTag("Gate");
    }

    [PunRPC]
    public void ResetObjects()
    {
        ChangePlayersScale(0.6f);
        ChangeBallScale(0.8f);

        if (ball.TryGetComponent(out ExplosiveBallController ballController))
        {
            PhotonNetwork.Destroy(ball);
            PhotonNetwork.Instantiate(Path.Combine("Balls", defaultBall.name), Vector3.zero, Quaternion.identity);
        }

        for (int i = 0; i < gates.Length; i++)
        {
            if ((gates[i].transform.childCount > 0 && gates[i].TryGetComponent(out WoodenGateController woodenGate)))
            {
                PhotonNetwork.Destroy(gates[i].transform.parent.gameObject);
            }
            else if (gates[i].TryGetComponent(out SteelGateController steelGateController))
            {
                PhotonNetwork.Destroy(gates[i]);
            }
            else
            {
                break;
            }

            if (i < normalGates.Length)
            {
                PhotonNetwork.Instantiate(Path.Combine("Gates", normalGates[i].name), Vector3.zero, Quaternion.identity);
            }
        }
    }

    public void SetGatesToParent()
    {
        FindGates();

        if (gates.Length == 2)
        {
            for (int i = 0; i < parents.Length; i++)
            {
                gates[i].transform.SetParent(parents[i]);
            }
        }

        if (gates.Length == 4)
        {
            gates[0].transform.parent.transform.SetParent(parents[0]);
            gates[2].transform.parent.transform.SetParent(parents[1]);
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

    [PunRPC]
    public void ExplosiveMode()
    {
        PhotonNetwork.Destroy(ball);
        PhotonNetwork.Instantiate(Path.Combine("Balls", explosiveBall.name), Vector3.zero, Quaternion.identity);
        SteelGates();
    }

    [PunRPC]
    void SteelGates()
    {
        for (int i = 0; i < gates.Length; i++)
        {
            PhotonNetwork.Destroy(gates[i]);
            PhotonNetwork.Instantiate(Path.Combine("Gates", steelGates[i].name), Vector3.zero, Quaternion.identity);
        }
    }

    [PunRPC]
    public void WoodenGates()
    {
        for (int i = 0; i < gates.Length; i++)
        {
            PhotonNetwork.Destroy(gates[i]);
            PhotonNetwork.Instantiate(Path.Combine("Gates", woodenGates[i].name), Vector3.zero, Quaternion.identity);
        }
    }
}
