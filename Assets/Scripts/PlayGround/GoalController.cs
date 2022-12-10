using UnityEngine;
using Photon.Pun;

public class GoalController : MonoBehaviour
{
    public bool trigger;
    public bool isGoal;
    public bool isRightSide;

    ScoreController _scoreController;
    PhotonView view;

    void Start()
    {
        if (!PhotonNetwork.IsMasterClient) Destroy(GetComponent<GoalController>());

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
        }

        if (!trigger)
        {
            isGoal = false;
        }
    }

    [PunRPC]
    void Goal()
    {
        _scoreController.AddPoints(isRightSide);
    }
}
