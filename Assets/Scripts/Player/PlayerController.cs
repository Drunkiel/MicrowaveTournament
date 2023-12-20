using Unity.Netcode;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    [SerializeField] private float moveForce;
    [SerializeField] private float jumpForce;
    [SerializeField] private Rigidbody rgBody;

    private bool isPlayerOne = true;
    public bool onTheGround;

    private void Start()
    {
        if (!IsOwner) return;
        PlayersCountServerRpc();
    }

    [ServerRpc(RequireOwnership = false)]
    private void PlayersCountServerRpc()
    {
        if (NetworkManager.ConnectedClientsIds.Count > 1)
        {
            isPlayerOne = false;
            RotatePlayer();
        }
    }

    private void Update()
    {
        if (!IsOwner) return;
        onTheGround = GetComponent<TriggerController>().isTriggered;
        if (Input.GetKeyDown(KeyCode.W)) Jump();
    }

    private void FixedUpdate()
    {
        if (!IsOwner) return;
        Movement();
    }

    private void Movement()
    {
        if (Input.GetKey(KeyCode.A) && !onTheGround) MovementSequence(isPlayerOne ? 1 : -1);

        if (Input.GetKey(KeyCode.D) && !onTheGround) MovementSequence(isPlayerOne ? -1 : 1);
    }

    private void MovementSequence(int a)
    {
        transform.Rotate(0.75f * a, 0, 0);
        rgBody.AddForce(moveForce * new Vector3(a, 0));
    }

    private void Jump()
    {
        //if (!onTheGround) return;
        rgBody.AddForce(new Vector3(0, jumpForce), ForceMode.Force);
    }

    private void RotatePlayer()
    {
        transform.SetPositionAndRotation(new Vector3(transform.position.x * -1, transform.position.y), Quaternion.Euler(0, 90, 0));
    }
}
