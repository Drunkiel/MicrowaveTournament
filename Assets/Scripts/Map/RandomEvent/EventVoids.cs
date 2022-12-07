using UnityEngine;
using Photon.Pun;

public class EventVoids : MonoBehaviour
{
    public GameObject[] players;

    //Ball
    public GameObject ball;
    public GameObject defaultBall;
    public GameObject explosiveBall;
    public GameObject discoBall;

    //Gates
    public GameObject[] actualGates;
    public GameObject[] normalGates;
    public GameObject[] woodenGates;
    public GameObject[] steelGates;
    public GameObject[] basketballGates;

    //Gates parent
    public Transform[] parents;

    public FlowOfTheGameController _gameController;
    public ScoreController _scoreController;

    void Start()
    {
        ball = _gameController.FindBall();
    }

    public void ChangePlayersScale(float scale)
    {
        _gameController.FindPlayers();

        foreach (GameObject player in players)
        {
            player.transform.localScale = new Vector3(scale, scale, scale);
        }
    }

    public void ChangeBallScale(float scale)
    {
        ball = _gameController.FindBall();

        ball.transform.localScale = new Vector3(scale, scale, scale);
    }

    public void ExplosiveMode()
    {
        _gameController.BallToSpawn(explosiveBall);
        _gameController.GatesToSpawn(steelGates[0], steelGates[1]);
    }

    public void WoodenGates()
    {
        _gameController.GatesToSpawn(woodenGates[0], woodenGates[1]);
    }

    public void BaketballGates()
    {
        _gameController.GatesToSpawn(basketballGates[0], basketballGates[1]);
    }

    public void DiscoMode()
    {
        _gameController.BallToSpawn(discoBall);
    }
}
