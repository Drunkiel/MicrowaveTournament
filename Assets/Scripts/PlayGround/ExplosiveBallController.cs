using UnityEngine;

public class ExplosiveBallController : MonoBehaviour
{
    public float cooldown;
    public float resCooldown;

    public Transform parent;
    public GameObject particle;

    BallController ballController;

    Rigidbody rgBody;

    // Start is called before the first frame update
    void Start()
    {
        rgBody = GetComponent<Rigidbody>();
        ballController = GetComponent<BallController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown <= 3)
        {
            ballController.StopBall();
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

        rgBody.AddForce(new Vector2(ballController.startVector.x * num, 0), ForceMode.Impulse);
    }
}
