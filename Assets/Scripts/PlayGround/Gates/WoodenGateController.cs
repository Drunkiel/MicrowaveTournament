using UnityEngine;
using Photon.Pun;

public class WoodenGateController : MonoBehaviour
{
    public int defects;

    PhotonView view;

    void Start()
    {
        view = GetComponent<PhotonView>();
    }

    public void Injuries()
    {
        switch (defects)
        {
            case 0:
                transform.GetChild(0).gameObject.SetActive(true);
                break;

            case 3:
                transform.GetChild(0).gameObject.SetActive(false);
                break;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Ball"))
        {
            view.RPC("AddDefects", RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    void AddDefects()
    {
        defects++;
        Injuries();
    }
}
