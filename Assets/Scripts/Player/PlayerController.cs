using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float knockout;
    public float jumpForce;
    public float moveForce;
    public float rotateSpeed;

    public bool playerOne;

    public bool onTheGround;
    public bool isDamaged;

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
        /*RotationFixer(playerOne);*/
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
        transform.Rotate(0.25f * rotateSpeed * num1, 0, 0);
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

    public void TakeDamage()
    {
        if (!isDamaged)
        {
            isDamaged = true;
        }
        else
        {
            gameObject.transform.position = new Vector3(100, 0, 0);
            isDamaged = false;
        }
    }

    void RotationFixer(bool isPlayerOne)
    {
        float rot;

        if (isPlayerOne)
        {
            rot = 90;
        }
        else
        {
            rot = -90;
        }

        if (transform.rotation.z < 0)
        {
            print("test");
            transform.rotation = Quaternion.Euler(transform.rotation.x, rot, 0);
        }
    }
}
