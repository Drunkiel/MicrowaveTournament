using UnityEngine;

public class BuildingGame : MonoBehaviour
{
    public PlayerController[] players;
    /*    public DesertMap desertMap;*/

    // Start is called before the first frame update
    void Start()
    {
        /*        MultiplyVariables();*/
    }

    void MultiplyVariables()
    {
        foreach (var player in players)
        {
            player.moveForce *= 10;
            player.rotateSpeed *= 3;
        }

        /*        desertMap.windSpeed *= 2;*/
    }
}
