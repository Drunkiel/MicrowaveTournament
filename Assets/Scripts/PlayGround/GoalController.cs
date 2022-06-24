using UnityEngine;

public class GoalController : MonoBehaviour
{
    public bool trigger;
    public bool isGoal;
    public bool isRightSide;

    public ScoreController scoreController;

    // Update is called once per frame
    void Update()
    {
        trigger = GetComponent<TriggerController>().isTriggered;

        if (trigger && !isGoal)
        {
            scoreController.AddPoints(isRightSide);
            isGoal = true;
        }

        if (!trigger)
        {
            isGoal = false;
        }
    }
}
