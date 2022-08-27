using UnityEngine;
using Photon.Pun;

public class ShootController : MonoBehaviour
{
    public float cooldown;
    private float resCooldown;
    public float chargedPower;
    public bool ballShotReady;
    public bool ballLaunched;

    public Transform trajectory;

    PhotonView view;
    DoorController _doorController;

    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
        _doorController = GetComponent<DoorController>();
        resCooldown = cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.LeftShift) && (GetComponent<PlayerController>().playerOne || !GetComponent<PlayerController>().playerOne)) && ballShotReady)
        {
            view.RPC("Shoot", RpcTarget.AllBuffered);
        }

        CheckStatus();
        Charge();
    }

    [PunRPC]
    void Shoot()
    {
        if (_doorController.isDoorOpen)
        {
            Vector3 difference = trajectory.position - transform.position;

            ballLaunched = true;
            chargedPower = 0;

            _doorController.ballCollider.GetComponent<Rigidbody>().velocity = Vector3.ClampMagnitude(new Vector2(difference.x * 10, difference.y * 10), 20);
        }
    }

    void CheckStatus()
    {
        if (chargedPower >= 3)
        {
            ballShotReady = true;
        }
        else
        {
            ballShotReady = false;
        }

        if (ballLaunched)
        {
            _doorController.isBallPicked = false;
        }

        if (ballLaunched)
        {
            if (cooldown <= 0)
            {
                ballLaunched = false;
                cooldown = resCooldown;
            }
            else
            {
                cooldown -= Time.deltaTime;
            }
        }
    }

    void Charge()
    {
        if (_doorController.isBallPicked && !_doorController.isDoorOpen && chargedPower <= 10)
        {
            chargedPower += Time.deltaTime * 1.2f;
        }
    }
}
