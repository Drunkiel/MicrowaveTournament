using UnityEngine;

public class LiftingController : MonoBehaviour
{
    public GameObject[] objectsToLift;

    // Start is called before the first frame update
    void Start()
    {
        DrawObjectToLift();
    }

    // Update is called once per frame
    void Update()
    {
        objectsToLift[0].GetComponent<FloorFragmentController>().Lift();
    }

    void DrawObjectToLift()
    {
        int num = Mathf.RoundToInt(Random.Range(0, objectsToLift.Length));

        print(num);
        objectsToLift[num].GetComponent<FloorFragmentController>().Lift();
    }
}
