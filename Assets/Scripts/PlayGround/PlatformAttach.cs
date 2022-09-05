using UnityEngine;

public class PlatformAttach : MonoBehaviour
{
    public string tag;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == tag) other.gameObject.transform.parent = transform;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == tag) other.gameObject.transform.parent = null;
    }
}
