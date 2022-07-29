using UnityEngine;

public class LiftingController : MonoBehaviour
{
    public GameObject[] objectsToLift;

    public float cooldown;
    private float resCooldown;

    // Start is called before the first frame update
    void Start()
    {
        resCooldown = cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown <= 0)
        {
            DrawObjectToLift();
            cooldown = resCooldown;
        }
        else
        {
            cooldown -= Time.deltaTime;
        }
    }

    void DrawObjectToLift()
    {
        int num = Mathf.RoundToInt(Random.Range(0, objectsToLift.Length));

        objectsToLift[num].GetComponent<FloorFragmentController>().Lift();
    }
}
