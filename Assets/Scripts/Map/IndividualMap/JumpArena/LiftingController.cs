using UnityEngine;

public class LiftingController : MonoBehaviour
{
    public GameObject[] objectsToLift;

    Rigidbody rgBody;

    // Start is called before the first frame update
    void Start()
    {
        DrawObjectToLift();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void DrawObjectToLift()
    {
        int num = Mathf.RoundToInt(Random.Range(0, objectsToLift.Length));

        print(num);
        Lift(num);
    }

    void Lift(int num)
    {
        objectsToLift[num].transform.Translate(Vector3.up * Time.deltaTime * 30);
    }
}
