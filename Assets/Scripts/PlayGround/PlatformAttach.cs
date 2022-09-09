using UnityEngine;

public class PlatformAttach : MonoBehaviour
{
    public string objectTag;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == objectTag) other.gameObject.transform.parent = transform;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == objectTag) other.gameObject.transform.parent = null;
    }
}
