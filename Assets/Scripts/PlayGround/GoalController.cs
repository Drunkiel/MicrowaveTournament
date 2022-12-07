using UnityEngine;
using Photon.Pun;

public class GoalController : MonoBehaviour
{
    public bool trigger;
    public bool isGoal;
    public bool goalBreak;
    public bool isRightSide;

    ScoreController _scoreController;
    PhotonView view;

    void Start()
    {
        view = GetComponent<PhotonView>();
        _scoreController = GameObject.FindGameObjectWithTag("CameraUI").transform.GetChild(0).GetComponent<ScoreController>();
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
            goalBreak = true;
        }

        if (!trigger)
        {
            isGoal = false;
            goalBreak = false;
        }
    }

    [PunRPC]
    void Goal()
    {
        if(!goalBreak) _scoreController.AddPoints(isRightSide);
    }
}
