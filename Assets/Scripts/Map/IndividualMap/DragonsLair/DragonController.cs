using UnityEngine;

public class DragonController : MonoBehaviour
{
    public GameObject ball;
    public bool moveRight;

    public float cooldown;

    //Animation things
    public bool isDoorOpen;
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown <= 0)
        {
            Shoot();
            cooldown = (int)Mathf.Round(Random.Range(6, 13));
        }
        else
        {
            Movement(moveRight);
            cooldown -= Time.deltaTime;
        }

        DoorController();
    }

    void Movement(bool isMovingRight)
    {
        if (isMovingRight)
        {
            transform.Translate(Vector3.right * Time.deltaTime * 2);
        }
        else
        {
            transform.Translate(Vector3.left * Time.deltaTime * 2);
        }

        if (transform.position.x < -6.4f || transform.position.x > 6.6f)
        {
            moveRight = !moveRight;
        }
    }

    void Shoot()
    {
        //Creating ball
        Instantiate(ball, transform.position, Quaternion.Euler(14, -180, 0));
    }

    void DoorController()
    {
        if (cooldown <= 3 && !isDoorOpen)
        {
            anim.Play("DoorOpen");
            isDoorOpen = true;
        }
        else if (cooldown > 3 && isDoorOpen)
        {
            anim.Play("DoorClose");
            isDoorOpen = false;
        }
    }
}
