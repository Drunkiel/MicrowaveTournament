using UnityEngine;

public class WoodenGateController : MonoBehaviour
{
    public int defects;
    public bool test;

    public bool isTriggered;

    // Update is called once per frame
    void Update()
    {
        isTriggered = GetComponent<TriggerController>().isTriggered;

        if (isTriggered)
        {
            Injuries();
        }
        else
        {
            test = false;
        }
    }

    public void Injuries()
    {
        switch (defects)
        {
            case 0:
                transform.GetChild(0).gameObject.SetActive(true);
                break;

            case 1:
                //Change material to yellow
                break;

            case 2:
                //Change material to red
                break;

            case 3:
                transform.GetChild(0).gameObject.SetActive(false);
                break;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Ball"))
        {
            if (!test)
            {
                defects++;
                test = true;
            }
        }
    }
}
