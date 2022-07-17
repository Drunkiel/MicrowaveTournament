using UnityEngine;

public class ShootController : MonoBehaviour
{
    public float cooldown;
    private float resCooldown;
    public float ejectionPower;
    public bool ballShotReady;
    public bool ballLaunched;

    DoorController _doorController;

    // Start is called before the first frame update
    void Start()
    {
        _doorController = GetComponent<DoorController>();
        resCooldown = cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && ballShotReady)
        {
            Shoot();
        }

        CheckStatus();
        Charge();
    }

    void Shoot()
    {
        if (_doorController.isDoorOpen)
        {
            ballLaunched = true;
            ejectionPower = 0;
            _doorController.ball.GetComponent<Rigidbody>().AddForce(new Vector2(ejectionPower / 2, ejectionPower / 5));
        }
    }

    void CheckStatus()
    {
        if (ejectionPower >= 3)
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
        if (_doorController.isBallPicked && !_doorController.isDoorOpen && ejectionPower <= 10)
        {
            ejectionPower += Time.deltaTime * 1.2f;
        }
    }
}
