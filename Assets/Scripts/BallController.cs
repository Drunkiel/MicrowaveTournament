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
}
