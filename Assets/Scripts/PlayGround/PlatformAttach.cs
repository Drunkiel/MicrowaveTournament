using UnityEngine;
using Photon.Pun;

public class PlatformAttach : MonoBehaviour
{
    public string objectTag;
    private Collider objectCollider;
    private Transform objectTransform;

    PhotonView view;

    void Start()
    {
        view = GetComponent<PhotonView>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == objectTag)
        {
            objectCollider = other;
            objectTransform = gameObject.transform;
            view.RPC("SetToObject", RpcTarget.AllBuffered);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == objectTag)
        {
            objectTransform = null;
            view.RPC("SetToObject", RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    void SetToObject()
    {
        objectCollider.gameObject.transform.parent = objectTransform;
    }
}
