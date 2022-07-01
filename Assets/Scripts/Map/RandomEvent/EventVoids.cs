using UnityEngine;

public class EventVoids : MonoBehaviour
{
    public GameObject[] players;
    public GameObject ball;

    public void ResetObjects()
    {
        ChangePlayersScale(0.6f);
        ChangeBallScale(0.8f);
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
}
