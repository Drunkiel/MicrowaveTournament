using UnityEngine;

public class DoorController : MonoBehaviour
{
    public bool isDoorOpen;
    public bool isBallPicked;
    public Transform tester;

    public LayerMask layer;
    public Collider ball;

    ShootController _shootController;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        _shootController = GetComponent<ShootController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && GetComponent<PlayerController>().playerOne || Input.GetKeyDown(KeyCode.RightShift) && !GetComponent<PlayerController>().playerOne)
        {
            Controller();
        }

        if (!isBallPicked && isDoorOpen)
        {
            SearchBall();
        }
        else if (isBallPicked && !_shootController.ballLaunched)
        {
            PickBall(ball);
        }
    }

    void Controller()
    {
        if (!isDoorOpen)
        {
            anim.Play("DoorOpen");
        }
        else
        {
            anim.Play("DoorClose");
        }

        isDoorOpen = !isDoorOpen;
    }

    void SearchBall()
    {
        Collider[] ballCollider = Physics.OverlapSphere(tester.position, 2, layer);

        if (ballCollider.Length > 0)
        {
            isBallPicked = true;
            ball = ballCollider[0];
        }
    }

    void PickBall(Collider ball)
    {
        ball.transform.position = new Vector3(tester.position.x, tester.position.y, ball.transform.position.z);
    }
}
