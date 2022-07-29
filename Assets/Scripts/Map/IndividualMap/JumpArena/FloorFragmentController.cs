using UnityEngine;

public class FloorFragmentController : MonoBehaviour
{
    public float maxHeightToLift;
    public float defaultHeight;

    public float cooldown;
    private float resCooldown;

    public bool hide;
    public LayerMask layer;

    // Start is called before the first frame update
    void Start()
    {
        maxHeightToLift = transform.position.y + 1.5f;
        defaultHeight = transform.position.y;
        resCooldown = cooldown;
    }

    void Update()
    {
        CheckPosition();
    }

    void CheckPosition()
    {
        if (transform.position.y >= maxHeightToLift)
        {
            if (cooldown <= 0)
            {
                hide = true;
                cooldown = resCooldown;
            }
            else
            {
                cooldown -= Time.deltaTime;
            }
        }
        else
        {
            hide = false;
        }

        if (hide)
        {
            Hide();
        }
    }

    void SendPlayerToHeaven()
    {
        Collider[] hitPlayer = Physics.OverlapBox(transform.position, new Vector3(0, 1, 0), Quaternion.identity, layer);

        foreach (Collider player in hitPlayer)
        {
            player.GetComponent<PlayerController>().JumpSequence(3);
        }
    }

    public void Lift()
    {
        SendPlayerToHeaven();

        while (transform.position.y <= maxHeightToLift)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * 4);
        }
    }

    public void Hide()
    {
        while (transform.position.y >= defaultHeight)
        {
            transform.Translate(Vector3.back * Time.deltaTime);
        }
    }
}
