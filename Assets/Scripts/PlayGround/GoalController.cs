using UnityEngine;
using Photon.Pun;

public class GoalController : MonoBehaviour
{
    public bool trigger;
    public bool isGoal;
    public bool isRightSide;

    public ScoreController scoreController;
    PhotonView view;

    void Start()
    {
        view = GetComponent<PhotonView>();
    }

    [PunRPC]
    // Update is called once per frame
    void Update()
    {
        trigger = GetComponent<TriggerController>().isTriggered;

        if (trigger && !isGoal)
        {
            view.RPC("Goal", RpcTarget.AllBuffered);
            isGoal = true;
        }

        if (!trigger)
        {
            isGoal = false;
        }
    }

    [PunRPC]
    void Goal()
    {
        scoreController.AddPoints(isRightSide);
    }
}
