using UnityEngine;

public class DoorController : MonoBehaviour
{
    public bool isDoorOpen;
    public Transform tester;

    public LayerMask layer;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Controller();
        }

        PickBall();
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

    void PickBall()
    {
        Collider[] ball = Physics.OverlapSphere(tester.position, 2, layer);

        ball[0].transform.position = new Vector3(tester.position.x, tester.position.y + 0.5f, ball[0].transform.position.z);
    }
}
