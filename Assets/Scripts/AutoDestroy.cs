using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public int timeToDestroy;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, timeToDestroy);
    }
}
