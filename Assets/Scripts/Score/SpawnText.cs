using UnityEngine;

public class SpawnText : MonoBehaviour
{
    public GameObject bluePoint;
    public GameObject redPoint;

    public void BlueScored()
    {
        Instantiate(bluePoint, bluePoint.transform.position, Quaternion.identity, this.gameObject.transform);
    }

    public void RedScored()
    {
        Instantiate(redPoint, redPoint.transform.position, Quaternion.identity, this.gameObject.transform);
    }
}
