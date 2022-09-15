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
    public GameObject discoBall;

    //Gates
    public GameObject[] gates;
    public GameObject[] normalGates;
    public GameObject[] woodenGates;
    public GameObject[] steelGates;
    public GameObject[] basketballGates;

    //Gates parent
    public Transform[] parents;

    public FlowOfTheGameController _gameController;
    public ScoreController _scoreController;

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
        _gameController.BallToSpawn(explosiveBall);
        SteelGates();
    }

    [PunRPC]
    public void WoodenGates()
    {
        _gameController.GatesToSpawn(woodenGates[0], woodenGates[1]);
    }

    [PunRPC]
    void SteelGates()
    {
        _gameController.GatesToSpawn(steelGates[0], steelGates[1]);
    }

    [PunRPC]
    public void BaketballGates()
    {
        _gameController.GatesToSpawn(basketballGates[0], basketballGates[1]);
    }

    [PunRPC]
    public void DiscoMode()
    {
        _gameController.BallToSpawn(discoBall);
    }
}
