using System.IO;
using UnityEngine;
using Photon.Pun;

public class FlowOfTheGameController : MonoBehaviour
{
    public EventVoids _eventVoids;

    // Start is called before the first frame update
    void Start()
    {
        FindBall();
        FindGates();
        FindPlayers();
    }

    public void FindBall()
    {
        _eventVoids.ball = GameObject.FindGameObjectWithTag("Ball");
        _eventVoids._scoreController._gameState._ballController = _eventVoids.ball.GetComponent<BallController>();
    }

    public void FindGates()
    {
        GameObject[] foundGates = GameObject.FindGameObjectsWithTag("Gate");

        for (int i = 0; i < foundGates.Length; i++)
        {
            if (foundGates[i] != null)
            {
                if (foundGates[i].transform.childCount > 0)
                {
                    _eventVoids.gates[0] = foundGates[i].transform.parent.gameObject;
                    _eventVoids.gates[1] = foundGates[i + 2].transform.parent.gameObject;
                    break;
                }

                _eventVoids.gates[0] = foundGates[i];
                _eventVoids.gates[1] = foundGates[++i];
                break;
            }
        }
    }

    public void FindPlayers()
    {
        GameObject[] foundPlayers = GameObject.FindGameObjectsWithTag("Player");
        _eventVoids.players = foundPlayers;
    }

    [PunRPC]
    public void ResetObjects()
    {
        //Reseting scale
        _eventVoids.ChangePlayersScale(0.6f);
        _eventVoids.ChangeBallScale(0.8f);

        if (_eventVoids.ball.TryGetComponent(out ExplosiveBallController ballController))
        {
            PhotonNetwork.Destroy(_eventVoids.ball);
            PhotonNetwork.Instantiate(Path.Combine("Balls", _eventVoids.defaultBall.name), Vector3.zero, Quaternion.identity);
        }

        //Destroying gates
        for (int i = 0; i < _eventVoids.gates.Length; i++)
        {
            Destroy(_eventVoids.gates[i]);
        }

        //Spawning new gates
        for (int i = 0; i < _eventVoids.gates.Length; i++)
        {
            if (i < _eventVoids.normalGates.Length)
            {
                PhotonNetwork.Instantiate(Path.Combine("Gates", _eventVoids.normalGates[i].name), Vector3.zero, Quaternion.identity);
            }
        }
    }

    public void SetGatesToParent()
    {
        FindGates();

        if (_eventVoids.gates.Length == 2)
        {
            for (int i = 0; i < _eventVoids.parents.Length; i++)
            {
                _eventVoids.gates[i].transform.SetParent(_eventVoids.parents[i]);
            }
        }

        if (_eventVoids.gates.Length == 4)
        {
            _eventVoids.gates[0].transform.parent.transform.SetParent(_eventVoids.parents[0]);
            _eventVoids.gates[2].transform.parent.transform.SetParent(_eventVoids.parents[1]);
        }
    }

    public void GatesToSpawn(string gateLeftName, string gateRightName)
    {
        string[] gatesToSpawn = new string[2] { gateLeftName, gateRightName };

        for (int i = 0; i < _eventVoids.gates.Length; i++)
        {
            PhotonNetwork.Destroy(_eventVoids.gates[i]);
            PhotonNetwork.Instantiate(Path.Combine("Gates", gatesToSpawn[i]), Vector3.zero, Quaternion.identity);
        }
    }

    public void BallToSpawn(string ballName)
    {
        PhotonNetwork.Destroy(_eventVoids.ball);
        PhotonNetwork.Instantiate(Path.Combine("Balls", ballName), Vector3.zero, Quaternion.identity);
    }

    public void DestroyParticles()
    {
        GameObject[] particles = GameObject.FindGameObjectsWithTag("Particle");

        for (int i = 0; i < particles.Length; i++)
        {
            Destroy(particles[i]);
        }
    }
}
