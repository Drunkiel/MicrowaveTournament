using UnityEngine;

public class LiftingController : MonoBehaviour
{
    public GameObject[] objectsToLift;
    public float maxHeightToLift;
    public float defaultHeight;

    // Start is called before the first frame update
    void Start()
    {
        maxHeightToLift = objectsToLift[0].transform.position.y + 1.5f;
        DrawObjectToLift();
    }

    // Update is called once per frame
    void Update()
    {
        Lift(0);
    }

    void DrawObjectToLift()
    {
        int num = Mathf.RoundToInt(Random.Range(0, objectsToLift.Length));

        print(num);
        Lift(num);
    }

    void Lift(int num)
    {
        while (objectsToLift[num].transform.position.y <= maxHeightToLift)
        {
            objectsToLift[num].transform.Translate(Vector3.forward * Time.deltaTime * 4);
        }
    }
}
