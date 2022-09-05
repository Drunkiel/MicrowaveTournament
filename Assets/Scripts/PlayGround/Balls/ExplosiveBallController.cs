using UnityEngine;

public class ExplosiveBallController : MonoBehaviour
{
    public float cooldown;
    public float resCooldown;
    public LayerMask layer;

    public Transform parent;
    public GameObject particle;

    BallController _ballController;

    Rigidbody rgBody;

    // Start is called before the first frame update
    void Start()
    {
        rgBody = GetComponent<Rigidbody>();
        _ballController = GetComponent<BallController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown <= 3)
        {
            _ballController.StopBall();
        }

        if (cooldown <= 0)
        {
            Explode();
            cooldown = resCooldown;
        }
        else
        {
            cooldown -= Time.deltaTime;
        }
    }

    void Explode()
    {
        Instantiate(particle, new Vector3(parent.position.x, parent.position.y, parent.position.z), Quaternion.identity);

        float num = Mathf.Round(Random.Range(-1, 1));

        if (num == 0)
        {
            num = -1;
        }

        Collider[] hitPlayer = Physics.OverlapSphere(parent.position, 2f, layer);

        foreach (Collider player in hitPlayer)
        {
            if (player.TryGetComponent(out PlayerController playerController))
            {
                playerController.TakeDamage();

                //Check if player lose and destroying gate
                if (playerController.transform.position.x > 50)
                {
                    GameObject[] gateOfLosedPlayer = GameObject.FindGameObjectsWithTag("Gate");

                    if (playerController.playerOne)
                    {
                        gateOfLosedPlayer[0].GetComponent<SteelGateController>().isDamaged = true;
                    }
                    else
                    {
                        gateOfLosedPlayer[1].GetComponent<SteelGateController>().isDamaged = true;
                    }
                }
            }

            if (player.TryGetComponent(out SteelGateController gateController))
            {
                gateController.isDamaged = true;
            }
        }

        rgBody.AddForce(new Vector2(_ballController.startVector.x * num, 0), ForceMode.Impulse);
    }
}
