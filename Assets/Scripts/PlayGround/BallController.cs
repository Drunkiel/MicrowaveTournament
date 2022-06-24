using UnityEngine;

public class BallController : MonoBehaviour
{
    public Vector3 startVector;
    Rigidbody rgBody;

    void Start()
    {
        rgBody = GetComponent<Rigidbody>();
        rgBody.AddForce(startVector, ForceMode.Impulse);
    }

    void Update()
    {
        //Controlling ball speed
        if (rgBody.velocity.magnitude >= 20)
        {
            rgBody.velocity = Vector3.ClampMagnitude(rgBody.velocity, 20);
        }
    }

    public void ResetBall()
    {
        transform.position = new Vector3(0, 2.2f, -0.3f);
    }
}
