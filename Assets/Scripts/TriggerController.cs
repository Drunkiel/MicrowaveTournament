using UnityEngine;

public class TriggerController : MonoBehaviour
{
    public bool isTriggered;
    public string Tag = "";

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag(Tag))
        {
            isTriggered = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag(Tag))
        {
            isTriggered = false;
        }
    }
}
