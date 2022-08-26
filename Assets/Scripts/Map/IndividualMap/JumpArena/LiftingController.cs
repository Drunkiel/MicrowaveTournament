using UnityEngine;
using Photon.Pun;

public class LiftingController : MonoBehaviour
{
    public GameObject[] objectsToLift;

    public float cooldown;
    private float resCooldown;

    PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
        resCooldown = cooldown;
        view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown <= 0)
        {
            view.RPC("DrawObjectToLift", RpcTarget.AllBuffered);
            cooldown = resCooldown;
        }
        else
        {
            cooldown -= Time.deltaTime;
        }
    }

    [PunRPC]
    void DrawObjectToLift()
    {
        if (!PhotonNetwork.IsMasterClient) return;

        int num = Mathf.RoundToInt(Random.Range(0, objectsToLift.Length));

        objectsToLift[num].GetComponent<FloorFragmentController>().Lift();
    }
}
