using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float knockout;
    public float jumpForce;
    public float moveForce;

    public bool playerOne;

    public bool onTheGround;

    Rigidbody rgBody;

    // Start is called before the first frame update
    void Start()
    {
        rgBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        onTheGround = GetComponent<TriggerController>().isTriggered;

        Movement();
        Jump();
    }

    void Movement()
    {
        //Player one
        if (Input.GetKey(KeyCode.A) && playerOne && !onTheGround)
        {
            MovementSequence(-1, -1);
        }

        if (Input.GetKey(KeyCode.D) && playerOne && !onTheGround)
        {
            MovementSequence(1, 1);
        }

        //Player two
        if (Input.GetKey(KeyCode.LeftArrow) && !playerOne && !onTheGround)
        {
            MovementSequence(1, -1);
        }

        if (Input.GetKey(KeyCode.RightArrow) && !playerOne && !onTheGround)
        {
            MovementSequence(-1, 1);
        }
    }

    //Num1 rotating, Num2 movement speed
    void MovementSequence(int num1, int num2)
    {
        transform.Rotate(0.25f * num1, 0, 0);
        knockout = 1 * num2;
        rgBody.AddForce(new Vector2(knockout * moveForce, 0));
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.W) && playerOne)
        {
            JumpSequence();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && !playerOne)
        {
            JumpSequence();
        }
    }

    void JumpSequence()
    {
        rgBody.AddForce(new Vector2(0, jumpForce));
    }
}
