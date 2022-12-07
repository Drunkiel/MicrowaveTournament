using UnityEngine;
using Photon.Pun;

public class FlowOfTheGameController : MonoBehaviour
{
    public EventVoids _eventVoids;

    // Start is called before the first frame update
    void Start()
    {
        FindGates();
        FindPlayers();
    }

    public GameObject FindBall()
    {
        return GameObject.FindGameObjectWithTag("Ball");
    }

    public void FindGates()
    {
        GameObject[] foundGates = GameObject.FindGameObjectsWithTag("Gate");

        for (int i = 0; i < foundGates.Length; i++)
        {
            if (foundGates[i] != null)
            {
                if (foundGates[i].transform.childCount <= 1 && !foundGates[i].name.Contains("Part"))
                {
                    _eventVoids.actualGates[0] = foundGates[i];
                    _eventVoids.actualGates[1] = foundGates[++i];
                    break;
                }

                if (foundGates[i].transform.parent.childCount > 1)
                {
                    _eventVoids.actualGates[0] = foundGates[i].transform.parent.gameObject;
                    _eventVoids.actualGates[1] = foundGates[i + 2].transform.parent.gameObject;
                    break;
                }
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
        FindPlayers();
        
        //Reseting scale
        _eventVoids.ChangePlayersScale(0.6f);
        _eventVoids.ChangeBallScale(0.8f);

        //Reseting ball
        BallToSpawn(_eventVoids.defaultBall);

        //Reseting gates
        GatesToSpawn(_eventVoids.normalGates[0], _eventVoids.normalGates[1]);
    }

    public void SetGatesToParent()
    {
        FindGates();

        if (_eventVoids.actualGates.Length == 2)
        {
            for (int i = 0; i < _eventVoids.parents.Length; i++)
            {
                _eventVoids.actualGates[i].transform.SetParent(_eventVoids.parents[i]);
            }
        }

        if (_eventVoids.actualGates.Length == 4)
        {
            _eventVoids.actualGates[0].transform.parent.transform.SetParent(_eventVoids.parents[0]);
            _eventVoids.actualGates[2].transform.parent.transform.SetParent(_eventVoids.parents[1]);
        }
    }

    public void GatesToSpawn(GameObject gateLeftName, GameObject gateRightName)
    {
        GameObject[] gatesToSpawn = new GameObject[2] { gateLeftName, gateRightName };

        for (int i = 0; i < _eventVoids.actualGates.Length; i++)
        {
            Destroy(_eventVoids.actualGates[i]);
            Instantiate(gatesToSpawn[i], gatesToSpawn[i].transform.position, Quaternion.identity);
        }
    }

    public void BallToSpawn(GameObject ballToSpawn)
    {
        GameObject[] allBalls = GameObject.FindGameObjectsWithTag("Ball");
        foreach (GameObject singleBall in allBalls)
        {
            Destroy(singleBall);
        }

        Instantiate(ballToSpawn, ballToSpawn.transform.position, Quaternion.identity);
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
