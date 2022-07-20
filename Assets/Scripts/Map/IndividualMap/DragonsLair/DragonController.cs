using UnityEngine;

public class DragonController : MonoBehaviour
{
    public GameObject[] players;
    public GameObject ball;

    public float cooldown;
    private float resCooldown;

    // Start is called before the first frame update
    void Start()
    {
        resCooldown = cooldown;
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown <= 0)
        {
            Shoot();
            cooldown = resCooldown;
        }
        else
        {
            cooldown -= Time.deltaTime;
        }
    }

    void Shoot()
    {
        int num = (int)Mathf.Round(Random.Range(0, 2));

        Vector3 difference = players[num].transform.position - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        print(rotZ);
        print(num);

        //Changes dragon rotation
        transform.rotation = Quaternion.Euler(14, 180, 0);

        //Creating ball
        Instantiate(ball, transform.position, Quaternion.Euler(14, rotZ, 0), gameObject.transform);
    }
}
