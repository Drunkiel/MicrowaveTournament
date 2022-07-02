using UnityEngine;

public class RandomEventController : MonoBehaviour
{
    public int randomNum;

    public EventVoids eventVoids;

    public void DrawNumber()
    {
        int num = (int)Mathf.Round(Random.Range(1, 6));

        if (num == 5)
        {
            randomNum = (int)Mathf.Round(Random.Range(1, 8));
        }
        else
        {
            randomNum = 0;
        }
    }

    public void PickEvent()
    {
        switch (randomNum)
        {
            case 0:
                eventVoids.ResetObjects();
                break;

            //Making player or ball small
            case 1:
                eventVoids.ChangePlayersScale(0.3f);
                break;

            case 2:
                eventVoids.ChangeBallScale(0.4f);
                break;

            case 3:
                eventVoids.ChangePlayersScale(0.3f);
                eventVoids.ChangeBallScale(0.4f);
                break;

            //Making player or ball big
            case 4:
                eventVoids.ChangePlayersScale(0.9f);
                break;

            case 5:
                eventVoids.ChangeBallScale(1f);
                break;

            case 6:
                eventVoids.ChangePlayersScale(0.9f);
                eventVoids.ChangeBallScale(1f);
                break;

            case 7:
                eventVoids.ExplosiveMode();
                break;
        }

    }
}
