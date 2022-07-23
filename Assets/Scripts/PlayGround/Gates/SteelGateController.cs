using UnityEngine;

public class SteelGateController : MonoBehaviour
{
    public bool isDamaged;

    // Update is called once per frame
    void Update()
    {
        if (isDamaged)
        {
            ChangePosition();
        }
    }

    void ChangePosition()
    {
        transform.position = new Vector3(-8.96f, 6, 0);
        isDamaged = false;
    }

    public void ResetPosition()
    {
        transform.position = new Vector3(-8.96f, -1.5f, 0);
    }
}
