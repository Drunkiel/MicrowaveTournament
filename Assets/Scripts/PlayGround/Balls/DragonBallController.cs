using UnityEngine;

public class DragonBallController : MonoBehaviour
{
    public bool isTriggered;
    public LayerMask layer;

    public GameObject particle;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3);
    }

    // Update is called once per frame
    void Update()
    {
        isTriggered = GetComponent<TriggerController>().isTriggered;

        if (isTriggered)
        {
            Expliosion();
        }

        transform.Translate(Vector3.forward * 10 * Time.deltaTime);
    }

    void Expliosion()
    {
        //Creating particle
        Instantiate(particle, Camera.main.ScreenToWorldPoint(transform.position), Quaternion.identity);

        //Giving damage to player
        Collider[] hitPlayer = Physics.OverlapSphere(transform.position, 1f, layer);

        foreach (Collider player in hitPlayer)
        {
            player.TryGetComponent(out PlayerController playerController);
            playerController.TakeDamage();
        }

        //Destroying gameobject
        Destroy(gameObject);
    }
}
