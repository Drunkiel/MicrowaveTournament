using UnityEngine;
using Photon.Pun;

public class DoorController : MonoBehaviour
{
    public bool isDoorOpen;
    public bool isBallPicked;
    public Transform tester;

    public LayerMask layer;
    public Collider ballCollider;

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
        ballCollider = GameObject.FindGameObjectWithTag("Ball").GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && view.IsMine)
        {
            view.RPC("Controller", RpcTarget.AllBuffered);
        }

        if (!isBallPicked && isDoorOpen)
        {
            SearchBall();
        }
        else if (isBallPicked && !_shootController.ballLaunched)
        {
            view.RPC("PickBall", RpcTarget.AllBuffered);
        }

        AutoShoot();
    }

    [PunRPC]
    void Controller()
    {
        if (!isDoorOpen)
        {
            anim.Play("DoorOpen");
            /*            audioSource.clip = audios[0];*/
            audioSource.Play();
        }
        else
        {
            anim.Play("DoorClose");
            /*            audioSource.clip = audios[1];*/
            audioSource.Play();
        }

        isDoorOpen = !isDoorOpen;
    }

    public void AutoShoot()
    {
        if (_shootController.chargedPower >= 10 && isDoorOpen)
        {
            view.RPC("Shoot", RpcTarget.AllBuffered);
        }
        else if (_shootController.chargedPower >= 10 && !isDoorOpen)
        {
            view.RPC("Controller", RpcTarget.AllBuffered);
            view.RPC("Shoot", RpcTarget.AllBuffered);
        }
    }

    void SearchBall()
    {
        Collider[] ballColliderFound = Physics.OverlapSphere(tester.position, 2, layer);

        if (ballColliderFound.Length > 0)
        {
            isBallPicked = true;
            ballCollider = ballColliderFound[0];
        }
    }

    [PunRPC]
    void PickBall()
    {
        ballCollider.transform.position = new Vector2(tester.position.x, tester.position.y);
    }
}
