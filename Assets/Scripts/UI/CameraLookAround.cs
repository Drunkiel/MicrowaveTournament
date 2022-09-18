using UnityEngine;

public class CameraLookAround : MonoBehaviour
{
    public float speedOfRotate;
    private float yaw;
    private float pitch;

    // Update is called once per frame
    void Update()
    {
        LookAround();
    }

    void LookAround()
    {
        yaw += speedOfRotate * Input.GetAxis("Mouse X");
        pitch -= speedOfRotate * Input.GetAxis("Mouse Y");

        yaw = Mathf.Clamp(yaw, -2f, 2f);
        //the rotation range
        pitch = Mathf.Clamp(pitch, -2f, 1.5f);
        //the rotation range

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }
}
