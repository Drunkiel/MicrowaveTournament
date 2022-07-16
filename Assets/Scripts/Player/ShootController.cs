using UnityEngine;

[System.Serializable]
public class ShootController
{
    public float cooldown;
    private float resCooldown;
    public bool ballShot;

    DoorController _doorController;

    // Start is called before the first frame update
    void Start()
    {
        resCooldown = cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Shoot();
        }

        CheckStatus();
    }

    void Shoot()
    {
        if (_doorController.isBallPicked && _doorController.isDoorOpen)
        {
            ballShot = true;
            _doorController.ball.GetComponent<Rigidbody>().AddForce(new Vector2(5, 2));
        }
    }

    void CheckStatus()
    {
        if (ballShot)
        {
            if (cooldown <= 0)
            {
                ballShot = false;
                cooldown = resCooldown;
            }
            else
            {
                cooldown -= Time.deltaTime;
            }
        }
    }
}
