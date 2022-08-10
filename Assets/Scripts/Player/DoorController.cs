using UnityEngine;
using Photon.Pun;

public class DoorController : MonoBehaviour
{
    public bool isDoorOpen;
    public bool isBallPicked;
    public Transform tester;

    public LayerMask layer;
    public Collider ball;

    AudioSource audioSource;
    public AudioClip[] audios;

    ShootController _shootController;
    PhotonView view;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        _shootController = GetComponent<ShootController>();
        view = GetComponent<PhotonView>();
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && view.IsMine)
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
            audioSource.clip = audios[0];
            audioSource.Play();
        }
        else
        {
            anim.Play("DoorClose");
            audioSource.clip = audios[1];
            audioSource.Play();
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
