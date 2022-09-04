using UnityEngine;
using Photon.Pun;

public class TiltMap : MonoBehaviour
{
    public Transform platform;
    public Transform[] pilars;

    private bool liftRight;
    private bool timeToReverse;

    public float cooldown;

    PhotonView view;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        if (!PhotonNetwork.IsMasterClient) Destroy(GetComponent<TiltMap>());
        view = GetComponent<PhotonView>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
    }

    void Timer()
    {
        if (cooldown <= 0)
        {
            view.RPC("Lift", RpcTarget.AllBuffered);
            cooldown = (int)Mathf.Round(Random.Range(4, 10));
        }
        else
        {
            cooldown -= Time.deltaTime;
        }
    }

    void DrawNumber()
    {
        timeToReverse = !timeToReverse;

        if (!timeToReverse)
        {
            float num = Mathf.Round(Random.Range(-1, 1));

            if (num >= -1 && num < 0)
            {
                liftRight = false;
            }

            if (num <= 1 && num >= 0)
            {
                liftRight = true;
            }
        }
    }

    [PunRPC]
    void Lift()
    {
        if (!PhotonNetwork.IsMasterClient) return;

        DrawNumber();

        if (timeToReverse)
        {
            if (!liftRight)
            {
                anim.Play("LiftLeft_Reverse");
            }
            else if (liftRight)
            {
                anim.Play("LiftRight_Reverse");
            }
        }
        else
        {
            if (!liftRight)
            {
                anim.Play("LiftLeft");
            }
            else if (liftRight)
            {
                anim.Play("LiftRight");
            }
        }
    }
}
