using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float knockout;
    public float jumpForce;
    public bool doubleJump;

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
        if(Input.GetKey(KeyCode.A) && !onTheGround){
            transform.Rotate(-0.25f, 0, 0);
            knockout = -1;
        }

        if(Input.GetKey(KeyCode.D) && !onTheGround){
            transform.Rotate(0.25f, 0, 0);
            knockout = 1;
        }
    }

    void Jump(){
        if(Input.GetKeyDown(KeyCode.W)){
            rgBody.AddForce(new Vector2(knockout * jumpForce/10, jumpForce));
        }
    }
}
