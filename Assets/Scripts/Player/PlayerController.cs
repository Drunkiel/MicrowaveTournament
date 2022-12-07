using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviourPunCallbacks
{
    public float knockout;
    public float jumpForce;
    public float moveForce;
    public float rotateSpeed;

    public bool playerOne;

    public bool onTheGround;
    public bool isDamaged;

    Rigidbody rgBody;
    PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
        CheckPlayer();
        rgBody = GetComponent<Rigidbody>();
        view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (view.IsMine)
        {
            onTheGround = GetComponent<TriggerController>().isTriggered;

            Movement();
            Jump();
        }
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
        if (Input.GetKey(KeyCode.A) && !playerOne && !onTheGround)
        {
            MovementSequence(1, -1);
        }

        if (Input.GetKey(KeyCode.D) && !playerOne && !onTheGround)
        {
            MovementSequence(-1, 1);
        }
    }

    //Num1 rotating, Num2 movement speed
    void MovementSequence(int num1, int num2)
    {
        transform.Rotate(0.25f * rotateSpeed * num1, 0, 0);
        /*transform.rotation = Quaternion.Euler(0.25f * rotateSpeed * num1, 0, 0);*/

        knockout = 1 * num2;
        rgBody.AddForce(new Vector2(knockout * moveForce, 0));
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            JumpSequence(1);
        }
    }

    public void JumpSequence(int multiplier)
    {
        rgBody.AddForce(new Vector2(0, jumpForce * multiplier));
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

    void CheckPlayer()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            playerOne = true;
        }
        else
        {
            playerOne = false;
        }
    }
}
