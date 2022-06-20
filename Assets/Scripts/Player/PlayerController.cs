using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float knockout;
    public float jumpForce;
    public bool doubleJump;

    public bool playerOne;

    public bool onTheGround;
    public Transform groundTester;
    public LayerMask layer;

    Rigidbody rgBody;

    // Start is called before the first frame update
    void Start()
    {
        rgBody = GetComponent<Rigidbody> ();
    }

    // Update is called once per frame
    void Update()
    {
        onTheGround = GetComponent<TriggerController>().isTriggered;

        Movement();
        Jump();
    }

    void Movement(){
        //Player one
        if(Input.GetKey(KeyCode.A) && playerOne && !onTheGround){
            MovementSequence(-1, -1);
        }

        if(Input.GetKey(KeyCode.D) && playerOne && !onTheGround){
            MovementSequence(1, 1);
        }

        //Player two
        if(Input.GetKey(KeyCode.LeftArrow) && !playerOne && !onTheGround)
        {
            MovementSequence(1, -1);
        }

        if (Input.GetKey(KeyCode.RightArrow) && !playerOne && !onTheGround)
        {
            MovementSequence(-1, 1);
        }
    }

    void MovementSequence(int num1, int num2)
    {
        transform.Rotate(0.25f * num1, 0, 0);
        knockout = 1 * num2;
    }

    void Jump(){
        if(Input.GetKeyDown(KeyCode.W) && playerOne)
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
        rgBody.AddForce(new Vector2(knockout * jumpForce / 10, jumpForce));
    }
}
